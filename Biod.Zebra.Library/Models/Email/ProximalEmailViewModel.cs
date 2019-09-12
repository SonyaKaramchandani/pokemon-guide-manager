using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.AspNet.Identity;
using Constants = Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Zebra.Library.Models.Email
{
    public class ProximalEmailViewModel : EmailViewModel
    {
        public string AoiLocationNames { get; set; }

        public string ShortLocationNames { get; set; }

        public string LongLocationNames { get; set; }

        public string DeltaRepCases { get; set; }

        public DateTime? EventDate { get; set; }

        public string EventDateString { get; set; }

        public string DiseaseName { get; set; }

        public string TotalCases { get; set; }

        public override string GetViewFilePath()
        {
            return "~/Views/Home/ProximalEmail.cshtml";
        }

        public override int GetEmailType()
        {
            return Constants.EmailTypes.PROXIMAL_EMAIL;
        }

        public static List<ProximalEmailViewModel> GetProximalEmailViewModelList(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager, int eventId)
        {
            var result = new List<ProximalEmailViewModel>();

            var users = userManager.Users.ToList();
            var eventList = dbContext.usp_ZebraEmailGetProximalEmailData(eventId).AsQueryable();

            var eventsGroupedByUser = eventList.GroupBy(e => e.UserId).ToList();

            foreach (var group in eventsGroupedByUser)
            {
                var user = users.FirstOrDefault(u => u.Id == group.Key);
                if (user == null || !user.NewCaseNotificationEnabled || userManager.IsInRole(user.Id,
                        ConfigurationManager.AppSettings.Get("UnsubscribedUsersRole")))
                {
                    continue;
                }

                var aoiLocations = dbContext.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds).Select(l => l.DisplayName);
                var oldestEventDate = group.Where(e => e.EventDate != null).OrderBy(e => e.EventDate).FirstOrDefault()?.EventDate;

                var viewModel = new ProximalEmailViewModel()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    DoNotTrackEnabled = user.DoNotTrackEnabled,
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

                result.Add(viewModel);
            }
            

            return result;
        }

        public static string ConstructTitle(ProximalEmailViewModel viewModel)
        {
            var diseaseName = viewModel.DiseaseName;
            var number = viewModel.DeltaRepCases;
            var locationNames = viewModel.ShortLocationNames;
            var date = viewModel.EventDateString;

            return $"Local {diseaseName} activity — {number} {diseaseName} cases reported in {locationNames} since {date}";
        }

        public static string FormatShortLocation(string[] locations)
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

        public static string FormatLongLocation(string[] locations)
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
