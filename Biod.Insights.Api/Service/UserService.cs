using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Account;
using Biod.Insights.Api.Models.User;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;
        private readonly IGeonameService _geonameService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserLocationService _userLocationService;

        /// <summary>
        /// User service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseRelevanceService">the disease relevance service</param>
        /// <param name="geonameService">the geoname service</param>
        /// <param name="userRoleService">the user role service</param>
        /// <param name="userLocationService">the user location service</param>
        public UserService(
            BiodZebraContext biodZebraContext,
            ILogger<UserService> logger,
            IDiseaseRelevanceService diseaseRelevanceService,
            IGeonameService geonameService,
            IUserRoleService userRoleService,
            IUserLocationService userLocationService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseRelevanceService = diseaseRelevanceService;
            _geonameService = geonameService;
            _userRoleService = userRoleService;
            _userLocationService = userLocationService;
        }

        public async Task<UserModel> GetUser(string userId)
        {
            var userResult = (await new UserQueryBuilder(_biodZebraContext)
                    .SetUserId(userId)
                    .BuildAndExecute())
                .FirstOrDefault();

            if (userResult == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {userId} does not exist");
            }

            return await ConvertToModel(userResult);
        }

        public async Task<UserModel> UpdatePersonalDetails(string userId, UserPersonalDetailsModel personalDetailsModel)
        {
            var user = (await new UserQueryBuilder(_biodZebraContext, true)
                    .SetUserId(userId)
                    .BuildAndExecute())
                .FirstOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {userId} does not exist");
            }

            await UpdatePublicRole(user, personalDetailsModel.RoleId);

            // Update Location
            if (user.User.GeonameId != personalDetailsModel.LocationGeonameId)
            {
                var location = await _geonameService.GetGeoname(personalDetailsModel.LocationGeonameId);
                user.User.GeonameId = location.GeonameId;
                user.User.Location = location.FullDisplayName;
                user.User.GridId = await _geonameService.GetGridIdByGeonameId(location.GeonameId);
            }

            user.User.FirstName = personalDetailsModel.FirstName;
            user.User.LastName = personalDetailsModel.LastName;
            user.User.Organization = personalDetailsModel.Organization;
            user.User.Email = personalDetailsModel.Email;
            user.User.PhoneNumber = personalDetailsModel.PhoneNumber;

            var saved = await _biodZebraContext.SaveChangesAsync();

            return await (saved != 0 ? GetUser(userId) : ConvertToModel(user));
        }

        public async Task<UserModel> UpdateCustomSettings(string userId, UserCustomSettingsModel customSettingsModel)
        {
            var user = (await new UserQueryBuilder(_biodZebraContext, true)
                    .SetUserId(userId)
                    .BuildAndExecute())
                .FirstOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {userId} does not exist");
            }

            await UpdatePublicRole(user, customSettingsModel.RoleId);
            await UpdateAois(user, customSettingsModel.GeonameIds.ToArray());

            var saved = await _biodZebraContext.SaveChangesAsync();

            return await (saved != 0 ? GetUser(userId) : ConvertToModel(user));
        }

        public async Task<UserModel> UpdateNotificationSettings(string userId, UserNotificationsModel notificationsModel)
        {
            var user = (await new UserQueryBuilder(_biodZebraContext, true)
                    .SetUserId(userId)
                    .BuildAndExecute())
                .FirstOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {userId} does not exist");
            }

            user.User.NewOutbreakNotificationEnabled = notificationsModel.IsEventEmailEnabled;
            user.User.NewCaseNotificationEnabled = notificationsModel.IsProximalEmailEnabled;
            user.User.WeeklyOutbreakNotificationEnabled = notificationsModel.IsWeeklyEmailEnabled;

            var saved = await _biodZebraContext.SaveChangesAsync();

            return await (saved != 0 ? GetUser(userId) : ConvertToModel(user));
        }

        public async Task UpdatePassword(string userId, ChangePasswordModel changePasswordModel)
        {
            throw new System.NotImplementedException();
        }

        private async Task<UserModel> ConvertToModel(UserJoinResult userResult)
        {
            var model = new UserModel
            {
                Id = userResult.User.Id,
                GroupId = userResult.User.UserGroupId,
                PersonalDetails = new UserPersonalDetailsModel
                {
                    FirstName = userResult.User.FirstName,
                    LastName = userResult.User.LastName,
                    Organization = userResult.User.Organization,
                    Email = userResult.User.Email,
                    PhoneNumber = userResult.User.PhoneNumber
                },
                Location = await _geonameService.GetGeoname(userResult.User.GeonameId),
                Roles = userResult.Roles.Select(UserRoleService.ConvertToModel),
                IsDoNotTrack = userResult.User.DoNotTrackEnabled,
                NotificationsSetting = new UserNotificationsModel
                {
                    IsEventEmailEnabled = userResult.User.NewOutbreakNotificationEnabled ?? false,
                    IsProximalEmailEnabled = userResult.User.NewCaseNotificationEnabled ?? false,
                    IsWeeklyEmailEnabled = userResult.User.WeeklyOutbreakNotificationEnabled ?? false,
                }
            };
            model.DiseaseRelevanceSetting = await _diseaseRelevanceService.GetUserDiseaseRelevanceSettings(model);

            return model;
        }

        private async Task UpdateAois(UserJoinResult user, ICollection<int> geonameIds)
        {
            var existingGeonameIds = new HashSet<string>(user.User.AoiGeonameIds.Split(','));
            var newGeonameIds = new HashSet<string>(geonameIds.Select(g => g.ToString()));
            if (!existingGeonameIds.SetEquals(newGeonameIds))
            {
                await _userLocationService.SetAois(user.User, geonameIds);
            }
        }

        private async Task UpdatePublicRole(UserJoinResult user, string newRoleId)
        {
            // Update Public Role
            var existingPublicRole = user.Roles.First(r => r.IsPublic).Id;
            if (existingPublicRole != newRoleId)
            {
                var role = await _userRoleService.GetUserRole(newRoleId);
                if (role == null || !role.IsPublic)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, $"Requested role with id {newRoleId} is not a valid role selection");
                }

                _biodZebraContext.AspNetUserRoles.Remove(new AspNetUserRoles {UserId = user.User.Id, RoleId = existingPublicRole});
                _biodZebraContext.AspNetUserRoles.Add(new AspNetUserRoles {UserId = user.User.Id, RoleId = newRoleId});
            }
        }
    }
}