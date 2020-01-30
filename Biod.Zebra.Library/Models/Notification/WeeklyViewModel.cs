using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.Notification;
using Biod.Zebra.Library.Models.Notification.Email;
using Biod.Zebra.Library.Models.Notification.Push;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using static Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Zebra.Library.Models.Notification
{
    public class WeeklyViewModel : EmailViewModel, IPushViewModel
    {
        private const string NOTIFICATION_TITLE = "Weekly Brief";
        private const string NOTIFICATION_SUMMARY = "{0} events relevant to your area(s) of interest";

        public override string ViewFilePath => "~/Views/Home/WeeklyEmail.cshtml";

        public override int NotificationType => EmailTypes.WEEKLY_BRIEF_EMAIL;

        public bool IsPaid { get; set; }

        public string AoiLocationNames { get; set; }

        public List<WeeklyEmailEventViewModel> LocalEvents { get; set; }

        public List<WeeklyEmailEventViewModel> NonLocalEvents { get; set; }

        public List<string> DeviceTokenList { get; set; } = new List<string>();

        public string Summary { get; set; }

        public int NotificationId { get; set; }

        public static List<NotificationViewModel> GetNotificationViewModelList(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager)
        {
            var result = new List<NotificationViewModel>();
            var eventList = dbContext.usp_ZebraEmailGetWeeklyEmailData().AsQueryable();
            var allUsers = userManager.Users.ToList();
            var eventsGroupedByUser = eventList.GroupBy(e => e.UserId).ToList();

            foreach (var user in allUsers)
            {
                if (!user.WeeklyOutbreakNotificationEnabled || !NotificationHelper.ShouldSendNotification(userManager, user))
                {
                    continue;
                }

                var group = eventsGroupedByUser.FirstOrDefault(u => u.Key == user.Id);

                WeeklyViewModel viewModel;
                if (group == null)
                {
                    var isPaid = userManager.IsInRole(user.Id, ConfigurationManager.AppSettings.Get("PaidUsersRole"));
                    var aoiLocationNames = dbContext.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds).AsQueryable();

                    // No events for this user
                    viewModel = new WeeklyViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        IsPaid = isPaid,
                        DoNotTrackEnabled = user.DoNotTrackEnabled,
                        EmailConfirmed = user.EmailConfirmed,
                        AoiLocationNames = string.Join(", ", aoiLocationNames.Select(g => g.DisplayName).ToList()),
                        AoiGeonameIds = user.AoiGeonameIds,
                        LocalEvents = new List<WeeklyEmailEventViewModel>(),
                        NonLocalEvents = new List<WeeklyEmailEventViewModel>()
                    };
                }
                else
                {
                    // Certain fields are the same among all events, use the first event to populate those fields
                    var firstEvent = group.First();

                    var eventsGroupedByLocal = group.GroupBy(e => e.LocalSpread).ToArray();

                    viewModel = new WeeklyViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        IsPaid = firstEvent.IsPaidUser ?? false,
                        DoNotTrackEnabled = user.DoNotTrackEnabled,
                        EmailConfirmed = user.EmailConfirmed,
                        AoiLocationNames = firstEvent.UserAoiLocationNames,
                        AoiGeonameIds = user.AoiGeonameIds,
                        LocalEvents = eventsGroupedByLocal.FirstOrDefault(g => g.Key == 1)? // LocalSpread = 1
                            .OrderByDescending(e => e.RepCases ?? 0)
                            .Where(e => e.RelevanceId != 3) // Relevance ID = 3 denotes DO NOT NOTIFY
                            .Select(e => new WeeklyEmailEventViewModel()
                            {
                                EventId = e.EventId ?? 0,
                                EventTitle = e.EventTitle,
                                IsNewEvent = e.IsNewEvent ?? false,
                                NewRepCases = e.DeltaNewRepCases,
                                NewDeaths = e.DeltaNewDeaths,
                                RelevanceId = e.RelevanceId
                            })
                            .ToList() ?? new List<WeeklyEmailEventViewModel>(),
                        NonLocalEvents = eventsGroupedByLocal
                            .Where(g => g.Key == 0 || g.Key == null)     // LocalSpread = 0, AND LocalSpread = null (always notify)
                            .OrderByDescending(g => g.Key ?? -1)         // Show ones with risk before showing the ones with unlikely/negligible risk
                            .SelectMany(g =>
                            {
                                return g
                                    .Where(e => e.RelevanceId != 3)      // Relevance ID: 3 = Do not notify
                                    .Select(e =>
                                    {
                                        string avgProbString = "Unlikely",
                                            avgVolumeString = "Negligible";
                                        decimal avgProb = 0,
                                            avgVolume = 0;

                                        if (e.MinProb != null && e.MaxProb != null && e.MinVolume != null && e.MaxVolume != null)
                                        {
                                            // Has risk available
                                            avgProbString = StringFormattingHelper.FormatAverageProbability(e.MinProb.Value, e.MaxProb.Value, out avgProb);
                                            avgVolumeString = StringFormattingHelper.FormatAverage(e.MinVolume.Value, e.MaxVolume.Value, out avgVolume);
                                        }

                                        // Old data point defaults to current data point if there is no old one
                                        decimal avgProbOld = avgProb,
                                            avgVolumeOld = avgVolume;

                                        if (e.MinProbOld != null && e.MaxProbOld != null && e.MinVolumeOld != null && e.MaxVolumeOld != null)
                                        {
                                            // Has previous data available
                                            StringFormattingHelper.FormatAverageProbability(e.MinProbOld.Value, e.MaxProbOld.Value, out avgProbOld);
                                            StringFormattingHelper.FormatAverage(e.MinVolumeOld.Value, e.MaxVolumeOld.Value, out avgVolumeOld);
                                        }

                                        return new WeeklyEmailEventViewModel()
                                        {
                                            EventId = e.EventId ?? -1,
                                            EventTitle = e.EventTitle,
                                            MinProbability = e.MinProb ?? (decimal) -1,
                                            MaxProbability = e.MaxProb ?? (decimal) -1,
                                            MinVolume = e.MinVolume ?? (decimal) -1,
                                            MaxVolume = e.MaxVolume ?? (decimal) -1,
                                            AverageProbability = avgProb,
                                            AverageProbabilityString = avgProbString,
                                            AverageProbabilityDelta = (avgProb - avgProbOld).ToString(@"(+#\%);(-#\%); "),
                                            AverageVolume = avgVolume,
                                            AverageVolumeString = avgVolumeString,
                                            AverageVolumeDelta = (avgVolume - avgVolumeOld).ToString(@"(+#);(-#); "),
                                            IsNewEvent = e.IsNewEvent ?? false,
                                            NewRepCases = e.DeltaNewRepCases,
                                            NewDeaths = e.DeltaNewDeaths,
                                            RelevanceId = e.RelevanceId,
                                            HasModelRun = e.IsLocalOnly
                                        };
                                    })
                                    .Where(e => e.RelevanceId == 1 || e.RelevanceId == 2 && e.AverageProbability >= 1)     // Relevance ID: 1 = Always notify, 2 = Risk to my location(s)
                                    .OrderByDescending(vm => vm.AverageVolume)
                                    .ThenByDescending(vm => vm.AverageProbability)
                                    .ThenByDescending(vm => vm.EventId);
                            }).ToList()
                    };
                }

                // Add common properties
                viewModel.Title = NOTIFICATION_TITLE;
                viewModel.Summary = string.Format(NOTIFICATION_SUMMARY, viewModel.LocalEvents.Count + viewModel.NonLocalEvents.Count);
                viewModel.DeviceTokenList = ExternalIdentifierHelper.GetExternalIdentifier(dbContext, ExternalIdentifiers.FIREBASE_FCM, user.Id);

                // Add to the result list
                result.Add(viewModel);
            }

            return result;
        }
    }

    public class WeeklyEmailEventViewModel
    {
        public int EventId { get; set; }

        public string EventTitle { get; set; }

        public decimal MinProbability { get; set; }

        public decimal MaxProbability { get; set; }

        public decimal MinVolume { get; set; }

        public decimal MaxVolume { get; set; }

        public string AverageProbabilityString { get; set; }

        public string AverageProbabilityDelta { get; set; }

        public string AverageVolumeString { get; set; }

        public string AverageVolumeDelta { get; set; }

        public bool IsNewEvent { get; set; }

        public int NewRepCases { get; set; }

        public int NewDeaths { get; set; }

        public decimal AverageProbability { get; set; }

        public decimal AverageVolume { get; set; }

        public int RelevanceId { get; set; }

        public bool HasModelRun { get; set; }
    }
}