using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Account;
using Biod.Insights.Service.Models.User;
using Biod.Insights.Common.Exceptions;
using Biod.Insights.Service.Configs;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
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

        public async Task<UserModel> GetUser(UserConfig config)
        {
            var userResult = (await new UserQueryBuilder(_biodZebraContext, config).BuildAndExecute()).FirstOrDefault();
            if (userResult == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {config.UserId} does not exist");
            }

            return await ConvertToModel(userResult);
        }

        public async Task<IEnumerable<UserModel>> GetUsers(IQueryable<AspNetUsers> customQueryable = null)
        {
            var userResults = await new UserQueryBuilder(_biodZebraContext)
                    .OverrideInitialQueryable(customQueryable)
                    .BuildAndExecute();

            var users = new List<UserModel>();
            foreach (var userResult in userResults)
            {
                users.Add(await ConvertToModel(userResult));
            }

            return users;
        }

        public async Task<UserModel> UpdatePersonalDetails(UserConfig config, UserPersonalDetailsModel personalDetailsModel)
        {
            var user = (await new UserQueryBuilder(_biodZebraContext, config).BuildAndExecute()).FirstOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {config.UserId} does not exist");
            }

            await UpdatePublicRole(user, personalDetailsModel.RoleId);

            // Update Location
            if (user.User.GeonameId != personalDetailsModel.LocationGeonameId)
            {
                var geonameConfig = new GeonameConfig.Builder()
                    .AddGeonameId(personalDetailsModel.LocationGeonameId)
                    .Build();
                
                var location = await _geonameService.GetGeoname(geonameConfig);
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

            return await (saved != 0 ? GetUser(config.UserId) : ConvertToModel(user));
        }

        public async Task<UserModel> UpdateCustomSettings(UserConfig config, UserCustomSettingsModel customSettingsModel)
        {
            var user = (await new UserQueryBuilder(_biodZebraContext, config).BuildAndExecute()).FirstOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {config.UserId} does not exist");
            }

            await UpdatePublicRole(user, customSettingsModel.RoleId);
            await UpdateAois(user, customSettingsModel.GeonameIds.ToArray());

            var saved = await _biodZebraContext.SaveChangesAsync();

            return await (saved != 0 ? GetUser(config.UserId) : ConvertToModel(user));
        }

        public async Task<UserModel> UpdateNotificationSettings(UserConfig config, UserNotificationsModel notificationsModel)
        {
            var user = (await new UserQueryBuilder(_biodZebraContext, config).BuildAndExecute()).FirstOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {config.UserId} does not exist");
            }

            user.User.NewOutbreakNotificationEnabled = notificationsModel.IsEventEmailEnabled;
            user.User.NewCaseNotificationEnabled = notificationsModel.IsProximalEmailEnabled;
            user.User.WeeklyOutbreakNotificationEnabled = notificationsModel.IsWeeklyEmailEnabled;

            var saved = await _biodZebraContext.SaveChangesAsync();

            return await (saved != 0 ? GetUser(config.UserId) : ConvertToModel(user));
        }

        public async Task UpdatePassword(UserConfig config, ChangePasswordModel changePasswordModel)
        {
            throw new System.NotImplementedException();
        }

        private async Task<UserModel> GetUser(string userId)
        {
            return await GetUser(new UserConfig.Builder().SetUserId(userId).Build());
        }

        private async Task<UserModel> ConvertToModel(UserJoinResult userResult)
        {
            var model = new UserModel
            {
                Id = userResult.User.Id,
                GroupId = userResult.User.UserGroupId,
                AoiGeonameIds = userResult.User.AoiGeonameIds,
                PersonalDetails = new UserPersonalDetailsModel
                {
                    FirstName = userResult.User.FirstName,
                    LastName = userResult.User.LastName,
                    Organization = userResult.User.Organization,
                    Email = userResult.User.Email,
                    PhoneNumber = userResult.User.PhoneNumber
                },
                Location = await _geonameService.GetGeoname(new GeonameConfig.Builder().AddGeonameId(userResult.User.GeonameId).Build()),
                Roles = userResult.Roles.Select(UserRoleService.ConvertToModel),
                IsDoNotTrack = userResult.User.DoNotTrackEnabled,
                IsEmailConfirmed = userResult.User.EmailConfirmed,
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