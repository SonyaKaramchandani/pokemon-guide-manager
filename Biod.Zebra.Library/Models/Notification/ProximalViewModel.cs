using Biod.Zebra.Library.EntityModels;
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
    public class ProximalViewModel : EmailViewModel, IPushViewModel
    {
        public override string ViewFilePath => "~/Views/Home/ProximalEmail.cshtml";

        public override int NotificationType => EmailTypes.PROXIMAL_EMAIL;

        public string AoiLocationNames { get; set; }

        public string ShortLocationNames { get; set; }

        public string LongLocationNames { get; set; }

        public string DeltaRepCases { get; set; }

        public DateTime? EventDate { get; set; }

        public string EventDateString { get; set; }

        public string DiseaseName { get; set; }

        public string TotalCases { get; set; }

        public List<string> DeviceTokenList { get; set; } = new List<string>();

        public string Summary { get; set; }

        public int NotificationId { get; set; }

        public static List<NotificationViewModel> GetNotificationViewModelList(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager, int eventId)
        {
            var result = new List<NotificationViewModel>();

            var dataGroupedByUser = dbContext.usp_ZebraEmailGetProximalEmailData(eventId).GroupBy(e => e.UserId).ToList();

            // Get event summary
            var notificationEvent = dbContext.Events.FirstOrDefault(e => e.EventId == eventId);
            if (notificationEvent == null)
            {
                return result;
            }
            var summary = notificationEvent.Summary;

            var users = userManager.Users.ToList();
            foreach (var group in dataGroupedByUser)
            {
                var user = users.FirstOrDefault(u => u.Id == group.Key);
                if (user == null || !user.NewCaseNotificationEnabled || !NotificationHelper.ShouldSendNotification(userManager, user))
                {
                    continue;
                }

                var aoiLocations = dbContext.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds).Select(l => l.DisplayName);
                var oldestEventDate = group.Where(e => e.EventDate != null).OrderBy(e => e.EventDate).FirstOrDefault()?.EventDate;

                var viewModel = new ProximalViewModel()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    DoNotTrackEnabled = user.DoNotTrackEnabled,
                    EmailConfirmed = user.EmailConfirmed,
                    AoiGeonameIds = user.AoiGeonameIds,
                    AoiLocationNames = string.Join("; ", aoiLocations),
                    EventId = eventId,
                    DeltaRepCases = StringFormattingHelper.FormatInteger(group.Sum(e => e.DeltaRepCases ?? 0)),
                    EventDate = oldestEventDate,
                    EventDateString = StringFormattingHelper.FormatDateWithConditionalYear(oldestEventDate),
                    ShortLocationNames = FormatShortLocation(group.Select(e => e.LocationName).ToArray()),
                    LongLocationNames = FormatLongLocation(group.Select(e => e.LocationDisplayName).ToArray()),
                    DiseaseName = dbContext.usp_ZebraEventGetDiseaseNameByEventId(eventId).FirstOrDefault() ?? "",
                    TotalCases = StringFormattingHelper.FormatInteger(group.First().TotalCases ?? 0).ToLower()
                };
                viewModel.Title = ConstructTitle(viewModel);
                viewModel.Summary = summary;
                viewModel.DeviceTokenList = ExternalIdentifierHelper.GetExternalIdentifier(dbContext, ExternalIdentifiers.FIREBASE_FCM, user.Id);

                result.Add(viewModel);
            }

            return result;
        }

        private static string ConstructTitle(ProximalViewModel viewModel)
        {
            var diseaseName = viewModel.DiseaseName;
            var number = viewModel.DeltaRepCases;
            var locationNames = viewModel.ShortLocationNames;
            var cases = StringFormattingHelper.FormatWordAsPluralByWord("case", number);

            return $"Local {diseaseName} activity — {number} {diseaseName} {cases} reported in {locationNames}";
        }

        private static string FormatShortLocation(string[] locations)
        {
            if (locations.Length >= 3)
            {
                return "multiple local places";
            }
            if (locations.Length == 2)
            {
                return string.Join(" and ", locations);
            }
            if (locations.Length == 1)
            {
                return locations[0];
            }

            return "";
        }

        private static string FormatLongLocation(string[] locations)
        {
            if (locations.Length >= 3)
            {
                return string.Join("; ", locations.Take(locations.Length - 1)) + $"; and {locations[locations.Length - 1]}";
            }
            if (locations.Length == 2)
            {
                return string.Join("; and ", locations);
            }
            if (locations.Length == 1)
            {
                return locations[0];
            }

            return "";
        }
    }
}
