using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models.Proximal;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biod.Insights.Notification.Engine.Services.Proximal
{
    public class ProximalNotificationService : NotificationService<ProximalNotificationService>, IProximalNotificationService
    {
        private readonly IEventService _eventService;
        private readonly IGeonameService _geonameService;

        public ProximalNotificationService(
            ILogger<ProximalNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService,
            IEmailRenderingApiService emailRenderingApiService,
            IEventService eventService,
            IUserService userService,
            IGeonameService geonameService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService, userService)
        {
            _eventService = eventService;
            _geonameService = geonameService;
        }

        public async Task ProcessRequest(int eventId)
        {
            await SendEmails(CreateModel(eventId));
        }

        private async IAsyncEnumerable<ProximalViewModel> CreateModel(int eventId)
        {
            var eventModel = await _eventService.GetEvent(eventId, null, true);
            var updatedLocations = eventModel.UpdatedLocations.ToList();
            if (!updatedLocations.Any())
            {
                _logger.LogInformation($"Event with id {eventId} has no change in case counts. No e-mail will be sent for this event.");
                yield break;
            }

            var eventCountries = updatedLocations
                .GroupBy(l => l.CountryName)
                .Select(g => new {
                    g.First().GeonameId,
                    g.First().CountryName
                })
                .ToList();
            if (eventCountries.Count > 1)
            {
                _logger.LogWarning($"Event with id {eventId} has {eventCountries.Count()} countries associated with it: " +
                    $"{string.Join(",", eventCountries.Select(c => c.GeonameId))}");
            }

            var proximalUserAois = await _eventService.GetUsersAffectedByEvent(eventId);
            if (!proximalUserAois.Any())
            {
                _logger.LogInformation($"Event with id {eventId} has no proximal users. No e-mail will be sent for this event.");
                yield break;
            }

            var proximalUserIds = proximalUserAois.Keys.AsEnumerable();

            // Get the subset of users that should receive the Proximal Email
            var customQueryable = _biodZebraContext.AspNetUsers.Where(u =>
                u.NewCaseNotificationEnabled.Value
                && u.AspNetUserRoles.All(ur => ur.Role.Name != RoleName.UnsubscribedUsers.ToString())
                && proximalUserIds.Contains(u.Id));
            var proximalUsers = await _userService.GetUsers(customQueryable);

            var allUserGeonameIds = proximalUserAois.Values.SelectMany(d => d.Keys).Distinct().AsEnumerable();
            var geonames = (await _geonameService.GetGeonames(allUserGeonameIds))
                .ToDictionary(g => g.GeonameId);

            foreach (var user in proximalUsers)
            {
                if (!user.DiseaseRelevanceSetting.GetRelevantDiseases().Contains(eventModel.EventInformation.DiseaseId))
                {
                    // User is not interested in the disease for this event
                    continue;
                }

                // Find all event locations relevant to this user's aoi
                var userEventLocations = proximalUserAois[user.Id].Values
                    .SelectMany(g => g)
                    .Distinct()
                    .Join(
                        updatedLocations,
                        g => g,
                        u => u.GeonameId,
                        (g, u) => new ProximalEventLocationViewModel
                        {
                            LocationName = u.LocationName,
                            LocationType = u.LocationType,
                            CaseCountChange = u.CaseCounts
                        })
                    .ToList();
                if (!userEventLocations.Any())
                {
                    _logger.LogInformation($"User with id {user.Id} will not receive an e-mail because user's AOIs are not affected by any of the case count change. ");
                    continue;
                }

                var model = new ProximalViewModel
                {
                    UserId = user.Id,
                    EventId = eventId,
                    SentDate = DateTimeOffset.UtcNow,
                    IsDoNotTrackEnabled = user.IsDoNotTrack,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    AoiGeonameIds = user.AoiGeonameIds,
                    Email = user.PersonalDetails.Email,
                    DiseaseName = eventModel.DiseaseInformation.Name,
                    CountryName = eventCountries.FirstOrDefault()?.CountryName ?? "",
                    UserLocations = proximalUserAois[user.Id].Keys
                        .Select(gid => geonames[gid])
                        .OrderBy(g => g.LocationType)
                        .ThenBy(g => g.FullDisplayName)
                        .Select(g => g.FullDisplayName),
                    LastUpdatedDate = StringFormattingHelper.FormatDateWithConditionalYear(updatedLocations.OrderByDescending(l => l.EventDate).FirstOrDefault()?.EventDate),
                    EventLocations = userEventLocations
                };
                model.Title = ConstructTitle(model);

                yield return model;
            }
        }

        private string ConstructTitle(ProximalViewModel model)
        {
            return $"Local {model.DiseaseName} activity in {model.CountryName}";
        }
    }
}