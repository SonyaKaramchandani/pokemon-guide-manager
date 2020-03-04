using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models.NewEvent;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OutbreakPotentialCategory = Biod.Insights.Common.Constants.OutbreakPotentialCategory;

namespace Biod.Insights.Notification.Engine.Services.NewEvent
{
    public class NewEventNotificationService : NotificationService<NewEventNotificationService>, INewEventNotificationService
    {
        private readonly IEventService _eventService;
        private readonly IDiseaseService _diseaseService;

        public NewEventNotificationService(
            ILogger<NewEventNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService,
            IEmailRenderingApiService emailRenderingApiService,
            IEventService eventService,
            IDiseaseService diseaseService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService)
        {
            _eventService = eventService;
            _diseaseService = diseaseService;
        }

        public async Task ProcessRequest(int eventId)
        {
            await SendEmails(_notificationSettings, CreateModels(eventId));
        }

        private async IAsyncEnumerable<NewEventViewModel> CreateModels(int eventId)
        {
            var eventModel = await _eventService.GetEvent(eventId, null);
            var userLocations = await _eventService.GetUsersAffectedByEvent(eventModel);
            var allLocations = userLocations.Values.SelectMany(l => l).Distinct();
            var allUsers = userLocations.Keys.AsEnumerable();

            // Create look ups for relevant data
            var userInformation = await _biodZebraContext.AspNetUsers
                .Where(u => allUsers.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => new
                {
                    u.Id,
                    u.Email,
                    u.AoiGeonameIds,
                    u.DoNotTrackEnabled,
                    u.EmailConfirmed,
                });
            var risksByGeonames = await _biodZebraContext.EventImportationRisksByGeoname
                .Where(r =>
                    r.EventId == eventModel.EventInformation.Id
                    && allLocations.Contains(r.GeonameId))
                .Select(r => new
                {
                    r.GeonameId,
                    r.Geoname.DisplayName,
                    r.Geoname.LocationType,
                    r.MinProb,
                    r.MaxProb,
                    r.MinVolume,
                    r.MaxVolume
                })
                .ToListAsync();
            var outbreakPotentials = await _biodZebraContext.GeonameOutbreakPotential
                .Where(op =>
                    op.DiseaseId == eventModel.EventInformation.DiseaseId
                    && allLocations.Contains(op.GeonameId))
                .ToDictionaryAsync(op => op.GeonameId, op => op.OutbreakPotentialId);

            // Create the email view model for each user
            foreach (var kv in userLocations)
            {
                var (userId, geonameIds) = kv;
                var user = userInformation[userId];
                var userLocationModels = geonameIds.Join(
                        risksByGeonames,
                        ug => ug,
                        r => r.GeonameId,
                        (ug, r) => new NewEventLocationViewModel
                        {
                            GeonameId = ug,
                            LocationName = r.DisplayName,
                            LocationType = r.LocationType ?? (int) LocationType.Unknown,
                            OutbreakPotentialCategoryId = outbreakPotentials.ContainsKey(ug) ? outbreakPotentials[ug] : (int) OutbreakPotentialCategory.Unknown,
                            ImportationRisk = new RiskModel
                            {
                                IsModelNotRun = eventModel.ExportationRisk.IsModelNotRun,
                                MinProbability = (float) (r.MinProb ?? 0),
                                MaxProbability = (float) (r.MaxProb ?? 0),
                                MinMagnitude = (float) (r.MinVolume ?? 0),
                                MaxMagnitude = (float) (r.MaxVolume ?? 0)
                            }
                        })
                    .ToList();
                foreach (var userLocationModel in userLocationModels)
                {
                    userLocationModel.IsLocal = (await _diseaseService.GetDiseaseCaseCount(
                                                    eventModel.EventInformation.DiseaseId,
                                                    userLocationModel.GeonameId,
                                                    eventModel.EventInformation.Id)).ReportedCases > 0;
                }

                yield return new NewEventViewModel
                {
                    UserId = userId,
                    Email = user.Email,
                    AoiGeonameIds = user.AoiGeonameIds,
                    IsDoNotTrackEnabled = user.DoNotTrackEnabled,
                    IsEmailConfirmed = user.EmailConfirmed,
                    TotalUserLocations = user.AoiGeonameIds.Split(',').Length,
                    EventId = eventModel.EventInformation.Id,
                    Title = eventModel.EventInformation.Title,
                    Summary = eventModel.EventInformation.Summary,
                    ArticleSources = eventModel.Articles.Select(a => a.SourceName).Distinct(),
                    DiseaseInformation = eventModel.DiseaseInformation,
                    SentDate = DateTimeOffset.UtcNow,
                    ReportedCases = eventModel.CaseCounts.ReportedCases,
                    UserLocations = userLocationModels
                };
            }
        }
    }
}