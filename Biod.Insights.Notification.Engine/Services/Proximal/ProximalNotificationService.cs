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
using Microsoft.EntityFrameworkCore;
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

            // Update the history table to set the new history data point
            await _eventService.UpdateEventActivityHistory(eventId);
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
            
            var currentDate = DateTime.Now;
            var recentlyUpdatedLocations = updatedLocations
                .Where(l => currentDate.Subtract(l.EventDate).Days <
                            _notificationSettings.ProximalEmailRecentThresholdInDays)
                .ToList();
            if (!recentlyUpdatedLocations.Any())
            {
                _logger.LogInformation($"Event with id {eventId} has no recent (within {_notificationSettings.ProximalEmailRecentThresholdInDays} days) " +
                                       $"change in case counts. No e-mail will be sent for this event.");
                yield break;
            }

            var eventCountries = recentlyUpdatedLocations
                .GroupBy(l => l.CountryName)
                .Select(g => new
                {
                    g.First().GeonameId,
                    g.First().CountryName
                })
                .ToList();
            if (eventCountries.Count > 1)
            {
                _logger.LogWarning($"Event with id {eventId} has {eventCountries.Count()} countries associated with it: " +
                                   $"{string.Join(",", eventCountries.Select(c => c.GeonameId))}");
            }

            var proximalUserAois = await _eventService.GetUsersWithinEventLocations(eventId);
            if (!proximalUserAois.Any())
            {
                _logger.LogInformation($"Event with id {eventId} has no proximal users. No e-mail will be sent for this event.");
                yield break;
            }

            var proximalUserIds = proximalUserAois.Keys.AsEnumerable();

            // Get the subset of users that should receive the Proximal Email
            var customQueryable = GetQualifyingRecipients().Where(u => u.NewCaseNotificationEnabled.Value && proximalUserIds.Contains(u.Id));
            var proximalUsers = (await _userService.GetUsers(customQueryable))
                .Where(u => u.DiseaseRelevanceSetting.GetRelevantDiseases().Contains(eventModel.EventInformation.DiseaseId))
                .ToList(); // User is interested in the disease for this event

            var allUserGeonameIds = proximalUserAois.Values.SelectMany(d => d.Keys).Distinct().AsEnumerable();
            var geonames = (await _geonameService.GetGeonames(allUserGeonameIds))
                .ToDictionary(g => g.GeonameId);

            // Lookup for the most recent email sent to the user for this event
            var lastSentEventEmailLookup = await (
                    from n in _biodZebraContext.UserEmailNotification
                    where n.EventId == eventId
                          && proximalUsers.Select(u => u.Id).Contains(n.UserId)
                          && n.EmailType == (int) NotificationType.Proximal
                    group n by n.UserId
                    into g
                    select new
                    {
                        UserId = g.Key,
                        MostRecentSentDate = g.Max(e => e.SentDate)
                    })
                .ToDictionaryAsync(r => r.UserId, r => r.MostRecentSentDate);

            foreach (var user in proximalUsers)
            {
                // Find all event locations relevant to this user's aoi
                var userEventLocations = proximalUserAois[user.Id].Values
                    .SelectMany(g => g)
                    .Distinct()
                    .Join(
                        recentlyUpdatedLocations,
                        g => g,
                        u => u.GeonameId,
                        (g, u) => new ProximalEventLocationViewModel
                        {
                            GeonameId = u.GeonameId,
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
                
                // Find all user AOIs that are relevant to the updated event locations
                var userEventLocationGeonameIds = new HashSet<int>(userEventLocations.Select(u => u.GeonameId));
                var userAoiLocations = proximalUserAois[user.Id]
                    .Where(kvp => kvp.Value.Intersect(userEventLocationGeonameIds).Any())
                    .Select(kvp => geonames[kvp.Key])
                    .OrderBy(g => g.LocationType)
                    .ThenBy(g => g.FullDisplayName)
                    .Select(g => g.FullDisplayName);

                var lastUpdatedDate = lastSentEventEmailLookup.ContainsKey(user.Id)
                    ? new DateTime(Math.Max(lastSentEventEmailLookup[user.Id].Ticks, eventModel.PreviousActivityDate?.Ticks ?? 0)) // Take whichever is later: last email date or last activity
                    : eventModel.PreviousActivityDate;

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
                    UserLocations = userAoiLocations,
                    LastUpdatedDate = StringFormattingHelper.FormatDateWithConditionalYear(lastUpdatedDate),
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