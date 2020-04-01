using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models.Weekly;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Biod.Insights.Service.Models;

namespace Biod.Insights.Notification.Engine.Services.Weekly
{
    public class WeeklyNotificationService : NotificationService<WeeklyNotificationService>, IWeeklyNotificationService
    {
        private readonly ICaseCountService _caseCountService;

        public WeeklyNotificationService(
            ILogger<WeeklyNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService,
            IEmailRenderingApiService emailRenderingApiService,
            IUserService userService,
            ICaseCountService caseCountService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService, userService)
        {
            _caseCountService = caseCountService;
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
            var customQueryable = GetQualifyingRecipients().Where(u => u.WeeklyOutbreakNotificationEnabled.Value);
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
                            var eventRisks = defaultRiskQueryable
                                .OrderByDescending(er => er.LocalSpread)
                                .ThenByDescending(er => er.MaxProb)
                                .ThenByDescending(er => er.Event.LastUpdatedDate)
                                .Where(er => er.GeonameId == aoi.GeonameId)
                                .Select(r => new
                                {
                                    r.EventId,
                                    r.Event.DiseaseId,
                                    r.Event.EventTitle,
                                    r.Event.IsLocalOnly,
                                    r.LocalSpread,
                                    MaxProb = (float) (r.MaxProb ?? 0),
                                    MinProb = (float) (r.MinProb ?? 0),
                                    MaxVolume = (float) (r.MaxVolume ?? 0),
                                    MinVolume = (float) (r.MinVolume ?? 0),
                                    CurrentEventLocations = r.Event.XtblEventLocation.Select(el => new XtblEventLocationJoinResult
                                    {
                                        RepCases = el.RepCases ?? 0,
                                        ConfCases = el.ConfCases ?? 0,
                                        SuspCases = el.SuspCases ?? 0,
                                        Deaths = el.Deaths ?? 0,
                                        EventDate = el.EventDate,
                                        GeonameId = el.GeonameId,
                                        Admin1GeonameId = el.Geoname.Admin1GeonameId,
                                        CountryGeonameId = el.Geoname.CountryGeonameId ?? -1,
                                        LocationType = el.Geoname.LocationType ?? (int) LocationType.City
                                    }),
                                    PreviousEventLocations = r.Event.XtblEventLocationHistory.Where(lh => lh.EventDateType == (int) EventLocationHistoryDateType.Weekly).Select(el =>
                                        new XtblEventLocationJoinResult
                                        {
                                            RepCases = el.RepCases ?? 0,
                                            ConfCases = el.ConfCases ?? 0,
                                            SuspCases = el.SuspCases ?? 0,
                                            Deaths = el.Deaths ?? 0,
                                            EventDate = el.EventDate,
                                            GeonameId = el.GeonameId,
                                            Admin1GeonameId = el.Geoname.Admin1GeonameId,
                                            CountryGeonameId = el.Geoname.CountryGeonameId ?? -1,
                                            LocationType = el.Geoname.LocationType ?? (int) LocationType.City
                                        })
                                })
                                .Where(risk => // Filter by disease relevance settings (risk only should be local or risk >=1%)
                                    userModel.DiseaseRelevanceSetting.AlwaysNotifyDiseaseIds.Contains(risk.DiseaseId)
                                    || (userModel.DiseaseRelevanceSetting.RiskOnlyDiseaseIds.Contains(risk.DiseaseId)
                                        && (risk.LocalSpread == 1
                                            || risk.MaxProb > DiseaseRelevanceHelper.THRESHOLD
                                        )))
                                .ToList();

                            return new WeeklyLocationViewModel
                            {
                                GeonameId = aoi.GeonameId,
                                LocationType = aoi.LocationType ?? (int) LocationType.Unknown,
                                LocationName = aoi.DisplayName,
                                TotalEvents = eventRisks.Count,
                                Events = eventRisks
                                    .Take(_notificationSettings.WeeklyEmailTopEvents)
                                    .Select(risk =>
                                    {
                                        // Apply nesting
                                        var caseCounts = _caseCountService.GetCaseCountTree(risk.CurrentEventLocations.ToList());
                                        var caseCountsHistory = _caseCountService.GetCaseCountTree(risk.PreviousEventLocations.ToList());
                                        var deltaCaseCounts = _caseCountService.GetAggregatedIncreasedCaseCount(caseCountsHistory, caseCounts, false);

                                        return new WeeklyEventViewModel
                                        {
                                            EventId = risk.EventId,
                                            EventTitle = risk.EventTitle,
                                            DiseaseId = risk.DiseaseId,
                                            IsLocal = risk.LocalSpread == 1,
                                            ImportationRisk = new RiskModel
                                            {
                                                IsModelNotRun = risk.IsLocalOnly,
                                                MaxProbability = risk.MaxProb,
                                                MinProbability = risk.MinProb,
                                                MaxMagnitude = risk.MaxVolume,
                                                MinMagnitude = risk.MinVolume,
                                            },
                                            CaseCountChange = deltaCaseCounts.Sum(d => d.Value.GetNestedRepCaseCount())
                                        };
                                    })
                                    .AsEnumerable()
                            };
                        })
                        .Where(aoi => aoi.Events.Any()) // Remove any locations that have no events
                        .AsEnumerable()
                };
            }
        }
    }
}