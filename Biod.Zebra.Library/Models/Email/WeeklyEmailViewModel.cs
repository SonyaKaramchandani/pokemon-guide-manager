using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.AspNet.Identity;
using Constants = Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Zebra.Library.Models.Email
{
    public class WeeklyEmailViewModel : EmailViewModel
    {
        public bool IsPaid { get; set; }

        public string AoiLocationNames { get; set; }

        public List<WeeklyEmailEventViewModel> LocalEvents { get; set; }

        public List<WeeklyEmailEventViewModel> NonLocalEvents { get; set; }

        public override string GetViewFilePath()
        {
            return "~/Views/Home/WeeklyEmail.cshtml";
        }

        public override int GetEmailType()
        {
            return Constants.EmailTypes.WEEKLY_BRIEF_EMAIL;
        }

        public static List<WeeklyEmailViewModel> GetWeeklyEmailViewModelList(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager)
        {
            var result = new List<WeeklyEmailViewModel>();

            var eventList = dbContext.usp_ZebraEmailGetWeeklyEmailData().AsQueryable();

            var allUsers = userManager.Users.ToList();
            var eventsGroupedByUser = eventList.GroupBy(e => e.UserId).ToList();

            foreach (var user in allUsers)
            {
                if (!user.WeeklyOutbreakNotificationEnabled || userManager.IsInRole(user.Id,
                        ConfigurationManager.AppSettings.Get("UnsubscribedUsersRole")))
                {
                    // Don't send email for users that don't have this enabled or have unsubscribed
                    continue;
                }

                var group = eventsGroupedByUser.FirstOrDefault(u => u.Key == user.Id);

                if (group == null)
                {
                    var isPaid = userManager.IsInRole(user.Id, role: ConfigurationManager.AppSettings.Get("PaidUsersRole"));
                    var aoiLocationNames = dbContext.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds).AsQueryable();

                    // No events for this user
                    result.Add(new WeeklyEmailViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        IsPaid = isPaid,
                        DoNotTrackEnabled = user.DoNotTrackEnabled,
                        AoiLocationNames = string.Join(", ", aoiLocationNames.Select(g => g.DisplayName).ToList()),
                        AoiGeonameIds = user.AoiGeonameIds,
                        LocalEvents = new List<WeeklyEmailEventViewModel>(),
                        NonLocalEvents = new List<WeeklyEmailEventViewModel>(),
                        Title = "Weekly Brief"
                    });
                }
                else
                {
                    // Certain fields are the same among all events, use the first event to populate those fields
                    var firstEvent = group.First();

                    var eventsGroupedByLocal = group.GroupBy(e => e.LocalSpread).ToArray();

                    result.Add(new WeeklyEmailViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        IsPaid = firstEvent.IsPaidUser ?? false,
                        DoNotTrackEnabled = user.DoNotTrackEnabled,
                        AoiLocationNames = firstEvent.UserAoiLocationNames,
                        AoiGeonameIds = user.AoiGeonameIds,
                        LocalEvents = eventsGroupedByLocal.FirstOrDefault(g => g.Key == 1)? // LocalSpread = 1
                            .OrderByDescending(e => e.RepCases ?? 0)
                            .Select(e => new WeeklyEmailEventViewModel()
                            {
                                EventId = e.EventId ?? 0,
                                EventTitle = e.EventTitle,
                                IsNewEvent = e.IsNewEvent ?? false,
                                NewRepCases = e.DeltaNewRepCases,
                                NewDeaths = e.DeltaNewDeaths
                            })
                            .ToList() ?? new List<WeeklyEmailEventViewModel>(),
                        NonLocalEvents = eventsGroupedByLocal.FirstOrDefault(g => g.Key == 0)? // LocalSpread = 0
                            .Where(e => e.MaxProb != null && e.MinProb != null && e.MinVolume != null && e.MaxVolume != null && e.MaxProb > 0)
                            .Select(e =>
                            {
                                var avgProbString = StringFormattingHelper.FormatAverageProbability(e.MinProb.Value, e.MaxProb.Value, out decimal avgProb);
                                var avgVolumeString = StringFormattingHelper.FormatAverage(e.MinVolume.Value, e.MaxVolume.Value, out decimal avgVolume);

                                // Old data point defaults to current data point if there is no old one
                                decimal avgProbOld = avgProb,
                                        avgVolumeOld = avgVolume;

                                if (e.MinProbOld != null && e.MaxProbOld != null && e.MinVolumeOld != null && e.MaxVolumeOld != null)
                                {
                                    StringFormattingHelper.FormatAverageProbability(e.MinProbOld.Value, e.MaxProbOld.Value, out avgProbOld);
                                    StringFormattingHelper.FormatAverage(e.MinVolumeOld.Value, e.MaxVolumeOld.Value, out avgVolumeOld);
                                }

                                return new WeeklyEmailEventViewModel()
                                {
                                    EventId = e.EventId ?? -1,
                                    EventTitle = e.EventTitle,
                                    MinProbability = e.MinProb ?? (decimal)-1,
                                    MaxProbability = e.MaxProb ?? (decimal)-1,
                                    MinVolume = e.MinVolume ?? (decimal)-1,
                                    MaxVolume = e.MaxVolume ?? (decimal)-1,
                                    AverageProbability = avgProb,
                                    AverageProbabilityString = avgProbString,
                                    AverageProbabilityDelta = (avgProb - avgProbOld).ToString(@"(+#\%);(-#\%); "),
                                    AverageVolume = avgVolume,
                                    AverageVolumeString = avgVolumeString,
                                    AverageVolumeDelta = (avgVolume - avgVolumeOld).ToString(@"(+#);(-#); "),
                                    IsNewEvent = e.IsNewEvent ?? false,
                                    NewRepCases = e.DeltaNewRepCases,
                                    NewDeaths = e.DeltaNewDeaths
                                };
                            })
                            .Where(vm => vm.AverageProbability >= 1)
                            .OrderByDescending(vm => vm.AverageVolume)
                            .ThenByDescending(vm => vm.AverageProbability)
                            .ToList() ?? new List<WeeklyEmailEventViewModel>(),
                        Title = "Weekly Brief"
                    });
                }
            }

            return result;
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
        }
    }
}
