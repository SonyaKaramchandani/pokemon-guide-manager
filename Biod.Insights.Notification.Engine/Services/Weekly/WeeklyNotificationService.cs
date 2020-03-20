using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models.Weekly;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Biod.Insights.Service.Models;

namespace Biod.Insights.Notification.Engine.Services.Weekly
{
    public class WeeklyNotificationService : NotificationService<WeeklyNotificationService>, IWeeklyNotificationService
    {
        public WeeklyNotificationService(
            ILogger<WeeklyNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService,
            IEmailRenderingApiService emailRenderingApiService,
            IUserService userService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService, userService)
        {
        }

        public async Task ProcessRequest()
        {
            await SendEmails(CreateModels());
        }

        private async IAsyncEnumerable<WeeklyViewModel> CreateModels()
        {
            // Date Range formatting for this set of emails is same for all emails
            var endDate = DateTime.Now.AddDays(-1);
            var startDate = endDate.AddDays(-6);
            var dateRange = $"{startDate.ToString(endDate.Year == startDate.Year ? "MMMM d" : "MMMM d, yyyy")} - {endDate:MMMM d, yyyy}";

            // Get the subset of users that should receive the Weekly Email
            var customQueryable = _biodZebraContext.AspNetUsers.Where(u =>
                u.WeeklyOutbreakNotificationEnabled.Value
                && u.AspNetUserRoles.All(ur => ur.Role.Name != RoleName.UnsubscribedUsers.ToString()));
            var userModels = await _userService.GetUsers(customQueryable);

            foreach (var userModel in userModels)
            {
                var relevantDiseaseIds = userModel.DiseaseRelevanceSetting.GetRelevantDiseases();
                var userAoiGeonameIds = userModel.AoiGeonameIds.Split(',').Select(gid => Convert.ToInt32(gid));
                var defaultRiskQueryable = _biodZebraContext.EventImportationRisksByGeoname.Where(e =>
                    relevantDiseaseIds.Contains(e.Event.DiseaseId)
                    && e.Event.EndDate == null);
                
                var orderedLocations = await (
                        from er in defaultRiskQueryable.Where(er => userAoiGeonameIds.Contains(er.GeonameId))
                        group new {er.GeonameId, er.MaxVolume, er.Geoname.DisplayName, er.Geoname.LocationType} by new {er.GeonameId, er.Geoname.DisplayName, er.Geoname.LocationType}
                        into g
                        orderby g.Sum(x => x.MaxVolume) descending
                        select new {g.Key.GeonameId, g.Key.DisplayName, g.Key.LocationType})
                    .Take(_notificationSettings.WeeklyEmailTopLocations)
                    .ToListAsync();

                yield return new WeeklyViewModel
                {
                    UserId = userModel.Id,
                    AoiGeonameIds = userModel.AoiGeonameIds,
                    Email = userModel.PersonalDetails.Email,
                    IsDoNotTrackEnabled = userModel.IsDoNotTrack,
                    IsEmailConfirmed = userModel.IsEmailConfirmed,
                    DateRange = dateRange,
                    SentDate = DateTimeOffset.UtcNow,
                    UserLocations = orderedLocations.Select(aoi =>
                        {
                            var eventRisks = defaultRiskQueryable.Where(er => er.GeonameId == aoi.GeonameId);
                            return new WeeklyLocationViewModel
                            {
                                GeonameId = aoi.GeonameId,
                                LocationType = aoi.LocationType ?? (int) LocationType.Unknown,
                                LocationName = aoi.DisplayName,
                                TotalEvents = eventRisks.Count(),
                                Events = eventRisks
                                    .OrderByDescending(er => er.LocalSpread)
                                    .ThenByDescending(er => er.MaxProb)
                                    .ThenByDescending(er => er.Event.LastUpdatedDate)
                                    .Take(_notificationSettings.WeeklyEmailTopEvents)
                                    .Select(r => new
                                    {
                                        r.EventId,
                                        r.Event.EventTitle,
                                        r.Event.IsLocalOnly,
                                        r.LocalSpread,
                                        r.MaxProb,
                                        r.MinProb,
                                        r.MaxVolume,
                                        r.MinVolume,
                                        CurrentCases = r.Event.XtblEventLocation.Sum(l => l.RepCases ?? 0),
                                        HistoryCases = r.Event.XtblEventLocationHistory.Where(lh => lh.EventDateType == (int) EventLocationHistoryDateType.Weekly).Sum(l => l.RepCases ?? 0)
                                    })
                                    .AsEnumerable()
                                    .Select(risk => new WeeklyEventViewModel
                                    {
                                        EventId = risk.EventId,
                                        EventTitle = risk.EventTitle,
                                        IsLocal = risk.LocalSpread == 1,
                                        ImportationRisk = new RiskModel
                                        {
                                            IsModelNotRun = risk.IsLocalOnly,
                                            MaxProbability = (float) (risk.MaxProb ?? 0),
                                            MinProbability = (float) (risk.MinProb ?? 0),
                                            MaxMagnitude = (float) (risk.MaxVolume ?? 0),
                                            MinMagnitude = (float) (risk.MinVolume ?? 0),
                                        },
                                        CaseCountChange = Math.Max(0, risk.CurrentCases - risk.HistoryCases)
                                    })
                                    .AsEnumerable()
                            };
                        })
                        .AsEnumerable()
                };
            }
        }
    }
}