using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models.NewEvent;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Service.Helpers;
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
            IDiseaseService diseaseService,
            IUserService userService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService, userService)
        {
            _eventService = eventService;
            _diseaseService = diseaseService;
        }

        public async Task<bool> IsSentAlready(int eventId)
        {
            return await _biodZebraContext.UserEmailNotification.AnyAsync(e => e.EventId == eventId && e.EmailType == (int) NotificationType.NewEvent);
        }

        public async Task ProcessRequest(int eventId)
        {
            if (await IsSentAlready(eventId))
            {
                _logger.LogWarning($"The new event email has already been sent for event {eventId}");
                return;
            }
            
            await SendEmails(CreateModels(eventId));
            
            // Update the history table on new event so the history has a starting point
            await _eventService.UpdateEventActivityHistory(eventId);
        }

        private async IAsyncEnumerable<NewEventViewModel> CreateModels(int eventId)
        {
            var eventModel = await _eventService.GetEvent(eventId, null, false);

            // Start with all users that have this type of notification enabled
            var customQueryable = GetQualifyingRecipients().Where(u => u.NewOutbreakNotificationEnabled.Value);
            var userModels = (await _userService.GetUsers(customQueryable)).ToList();
            var allUserAois = new HashSet<int>(userModels.SelectMany(u => u.AoiGeonameIds.Split(',')).Select(g => Convert.ToInt32(g)));

            // Create look ups for risks and outbreak potential
            var risksByGeonames = await _biodZebraContext.EventImportationRisksByGeoname
                .Where(r =>
                    r.EventId == eventModel.EventInformation.Id
                    && (allUserAois.Contains(r.GeonameId)
                        || r.LocalSpread == 1
                        || r.MaxProb != null && r.MaxProb.Value > 0.01m))
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
            var allRiskLocations = new HashSet<int>(risksByGeonames.Select(r => r.GeonameId));
            var outbreakPotentials = await _biodZebraContext.GeonameOutbreakPotential
                .Where(op =>
                    op.DiseaseId == eventModel.EventInformation.DiseaseId
                    && allRiskLocations.Contains(op.GeonameId))
                .ToDictionaryAsync(op => op.GeonameId, op => op.OutbreakPotentialId);
            
            // Determine which locations are considered local to the event, using whether there are locally reported cases after nesting
            var isLocalDict = new Dictionary<int, bool>();
            foreach (var gid in allRiskLocations)
            {
                isLocalDict.Add(gid, (await _diseaseService.GetDiseaseCaseCount(
                    eventModel.EventInformation.DiseaseId,
                    gid,
                    eventModel.EventInformation.Id)).ReportedCases > 0);
            }

            // Create the email view model for each user
            foreach (var user in userModels)
            {
                if (!user.DiseaseRelevanceSetting.GetRelevantDiseases().Contains(eventModel.EventInformation.DiseaseId))
                {
                    // User is not interested in the disease for this event
                    continue;
                }

                var userAois = user.AoiGeonameIds.Split(',').Select(g => Convert.ToInt32(g)).ToList();
                var userLocationModels = userAois.Join(
                        risksByGeonames,
                        ug => ug,
                        r => r.GeonameId,
                        (ug, r) => new NewEventLocationViewModel
                        {
                            GeonameId = ug,
                            LocationName = r.DisplayName,
                            LocationType = r.LocationType ?? (int) LocationType.Unknown,
                            OutbreakPotentialCategoryId = outbreakPotentials.ContainsKey(ug) ? outbreakPotentials[ug] : (int) OutbreakPotentialCategory.Unknown,
                            IsLocal = isLocalDict[ug],
                            ImportationRisk = new RiskModel
                            {
                                IsModelNotRun = eventModel.ExportationRisk.IsModelNotRun,
                                MinProbability = (float) (r.MinProb ?? 0),
                                MaxProbability = (float) (r.MaxProb ?? 0),
                                MinMagnitude = (float) (r.MinVolume ?? 0),
                                MaxMagnitude = (float) (r.MaxVolume ?? 0),
                            }
                        })
                    .ToList();

                if (!userLocationModels.Any(ulm =>
                    user.DiseaseRelevanceSetting.AlwaysNotifyDiseaseIds.Contains(eventModel.EventInformation.DiseaseId)
                    || (user.DiseaseRelevanceSetting.RiskOnlyDiseaseIds.Contains(eventModel.EventInformation.DiseaseId)
                        && (ulm.IsLocal
                            || ulm.ImportationRisk.MaxProbability > DiseaseRelevanceHelper.THRESHOLD
                        ))))
                {
                    // User is not receiving this email due to no locations are relevant after filtering by disease relevance types 1 and 2.
                    // This means the disease is not an always notify disease or it is a risk but does not pass the minimum to notify
                    continue;
                }

                yield return new NewEventViewModel
                {
                    UserId = user.Id,
                    Email = user.PersonalDetails.Email,
                    AoiGeonameIds = user.AoiGeonameIds,
                    IsDoNotTrackEnabled = user.IsDoNotTrack,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    TotalUserLocations = userAois.Count,
                    EventId = eventModel.EventInformation.Id,
                    Title = eventModel.EventInformation.Title,
                    Summary = eventModel.EventInformation.Summary,
                    ArticleSources = eventModel.Articles.Select(a => a.SourceName).Distinct(),
                    DiseaseInformation = eventModel.DiseaseInformation,
                    SentDate = DateTimeOffset.UtcNow,
                    ReportedCases = eventModel.CaseCounts.ReportedCases,
                    UserLocations = userLocationModels
                        .OrderByDescending(ulm => ulm.IsLocal)
                        .ThenByDescending(ulm => ulm.ImportationRisk?.MaxProbability)
                        .ThenBy(ulm => ulm.LocationName)
                        .Take(_notificationSettings.EventEmailTopLocations)
                };
            }
        }
    }
}