using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models.Notification.Email;
using Biod.Zebra.Library.Models.Notification.Push;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using static Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Zebra.Library.Models.Notification
{
    public class ProximalViewModel : EmailViewModel, IPushViewModel
    {
        public override string ViewFilePath => "~/Views/Home/ProximalEmail.cshtml";

        public override int NotificationType => EmailTypes.PROXIMAL_EMAIL;

        public string AoiLocationNames { get; set; }

        public int EventGeonameId { get; set; }

        public string EventLocationName { get; set; }

        public int EventLocationType { get; set; }

        public string DeltaRepCases { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateDateString { get; set; }

        public string DiseaseName { get; set; }

        public List<string> DeviceTokenList { get; set; } = new List<string>();

        public string Summary { get; set; }

        public int NotificationId { get; set; }

        public static List<NotificationViewModel> GetNotificationViewModelList(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager, int eventId)
        {
            var result = new List<NotificationViewModel>();

            var proximalUsers = dbContext.usp_ZebraEventGetProximalUsersByEventId(eventId).ToList();
            if (!proximalUsers.Any())
            {
                return result;
            }

            var deltaCaseCounts = EventCaseCountModel.GetUpdatedCaseCountModelsForEvent(dbContext, eventId);
            if (!deltaCaseCounts.Any())
            {
                return result;
            }

            var currentEvent = dbContext.Events.FirstOrDefault(e => e.EventId == eventId);
            if (currentEvent == null)
            {
                return result;
            }

            var diseaseName = dbContext.usp_ZebraEventGetDiseaseNameByEventId(eventId).FirstOrDefault() ?? "";
            var lastUpdateDateByGeoname = dbContext.Xtbl_Event_Location_history
                .Where(e => e.EventId == eventId)
                .GroupBy(e => e.GeonameId)
                .Select(g => g.OrderByDescending(c => c.EventDate).FirstOrDefault())
                .ToDictionary(e => e.GeonameId, e => e.EventDate);
            var caseCountGeonames = dbContext.ActiveGeonames
                // done to address this error: "LINQ to Entities does not recognize the method 'Boolean ContainsKey(Int32)' method, 
                // and this method cannot be translated into a store expression.
                .AsEnumerable()  
                .Where(n => deltaCaseCounts.ContainsKey(n.GeonameId))
                .ToDictionary(n => n.GeonameId, n => new { n.Name, n.LocationType });
            var emailByGeoname = deltaCaseCounts.ToDictionary(c => c.Key, c =>
            {
                var model = new ProximalViewModel
                {
                    EventId = eventId,
                    DiseaseName = diseaseName,
                    Summary = currentEvent.Summary ?? "",
                    EventGeonameId = c.Key,
                    EventLocationName = caseCountGeonames[c.Key].Name,
                    EventLocationType = caseCountGeonames[c.Key].LocationType ?? LocationType.CITY,
                    DeltaRepCases = StringFormattingHelper.FormatInteger(c.Value.GetNestedCaseCount()),
                };
                model.Title = ConstructTitle(model);

                return model;
            });

            var users = userManager.Users.ToList();
            var filteredUsers = proximalUsers.Where(u => u.GeonameId != null && emailByGeoname.ContainsKey((int)u.GeonameId));
            foreach(var proximalUser in filteredUsers)
            {
                var user = users.FirstOrDefault(u => u.Id == proximalUser.UserId);
                if (user == null)
                {
                    continue;
                }

                var userAoiList = dbContext.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds).ToList();
                var userAoiDisplayNames = string.Join("; ", userAoiList.Select(l => l.DisplayName));
                if (proximalUser.GeonameId is int userGeonameId && emailByGeoname.ContainsKey(userGeonameId))
                {
                    var template = emailByGeoname[userGeonameId];
                    result.Add(new ProximalViewModel
                    {
                        EventId = template.EventId,
                        DiseaseName = template.DiseaseName,
                        Summary = template.Summary,
                        EventGeonameId = template.EventGeonameId,
                        EventLocationName = template.EventLocationName,
                        EventLocationType = template.EventLocationType,
                        DeltaRepCases = template.DeltaRepCases,
                        Title = template.Title,
                        UserId = user.Id,
                        Email = user.Email,
                        DoNotTrackEnabled = user.DoNotTrackEnabled,
                        EmailConfirmed = user.EmailConfirmed,
                        AoiGeonameIds = user.AoiGeonameIds,
                        AoiLocationNames = userAoiDisplayNames,
                        LastUpdateDate = lastUpdateDateByGeoname.ContainsKey(userGeonameId) ? lastUpdateDateByGeoname[userGeonameId] : (DateTime?)null,
                        LastUpdateDateString = lastUpdateDateByGeoname.ContainsKey(userGeonameId) ? StringFormattingHelper.FormatDateWithConditionalYear(lastUpdateDateByGeoname[userGeonameId]) : "",
                        DeviceTokenList = ExternalIdentifierHelper.GetExternalIdentifier(dbContext, ExternalIdentifiers.FIREBASE_FCM, user.Id)
                    });
                }
            }

            return result;
        }

        private static string ConstructTitle(ProximalViewModel viewModel)
        {
            var diseaseName = viewModel.DiseaseName;
            var number = viewModel.DeltaRepCases;
            var cases = StringFormattingHelper.FormatWordAsPluralByWord("case", number);

            return $"Local {diseaseName} activity — {number} new {diseaseName} {cases} of {diseaseName}";
        }
    }
}