using System;
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
using Biod.Products.Common.Exceptions;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Models;
using Biod.Products.Common.Constants;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;
        private readonly IGeonameService _geonameService;
        private readonly IUserTypeService _userTypeService;
        private readonly IUserLocationService _userLocationService;

        /// <summary>
        /// User service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseRelevanceService">the disease relevance service</param>
        /// <param name="geonameService">the geoname service</param>
        /// <param name="userTypeService">the user type service</param>
        /// <param name="userLocationService">the user location service</param>
        public UserService(
            BiodZebraContext biodZebraContext,
            ILogger<UserService> logger,
            IDiseaseRelevanceService diseaseRelevanceService,
            IGeonameService geonameService,
            IUserTypeService userTypeService,
            IUserLocationService userLocationService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseRelevanceService = diseaseRelevanceService;
            _geonameService = geonameService;
            _userTypeService = userTypeService;
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

            await UpdateUserType(user, personalDetailsModel.UserTypeId);

            // Update Location
            if (user.User.GeonameId != personalDetailsModel.LocationGeonameId)
            {
                var geonameConfig = new GeonameConfig.Builder()
                    .AddGeonameId(personalDetailsModel.LocationGeonameId)
                    .Build();

                var location = await _geonameService.GetGeoname(geonameConfig);
                user.User.GeonameId = location.GeonameId;
                user.User.Location = location.FullDisplayName;
                user.User.GridId = _geonameService.GetGridIdsByGeonameId(location.GeonameId).First(); // Cities should only have 1 Grid Id
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

            await UpdateUserType(user, customSettingsModel.UserTypeId);
            await UpdateAois(user, customSettingsModel.GeonameIds.ToArray());
            await UpdateDiseaseRelevance(user, customSettingsModel.DiseaseRelevanceSettings, customSettingsModel.IsPresetSelected);

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
                UserType = userResult.UserType != null
                    ? new UserTypeModel
                    {
                        Id = userResult.UserType.Id,
                        Name = userResult.UserType.Name,
                        NotificationDescription = userResult.UserType.NotificationDescription
                    }
                    : null,
                Roles = userResult.Roles.Select(role => new UserRoleModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsPublic = role.IsPublic
                }),
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

        private async Task UpdateUserType(UserJoinResult user, Guid? newUserTypeId)
        {
            var currentUserType = user.UserType;
            if (currentUserType.Id != newUserTypeId)
            {
                var userType = newUserTypeId.HasValue ? await _userTypeService.GetUserType(newUserTypeId.Value) : null;
                if (userType == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, $"Requested user type with id {newUserTypeId} is not a valid selection");
                }

                user.UserType.Id = userType.Id;
            }
        }

        private async Task UpdateDiseaseRelevance(UserJoinResult user, DiseaseRelevanceSettingsModel diseaseRelevanceSettings, bool isPreset = false)
        {
            // Remove all entries for the user in User_Disease_Relevance cross table
            _biodZebraContext.XtblUserDiseaseRelevance.RemoveRange(_biodZebraContext.XtblUserDiseaseRelevance.Where(u => u.UserId == user.User.Id));

            if (!isPreset)
            {
                await _biodZebraContext.XtblUserDiseaseRelevance.AddRangeAsync(diseaseRelevanceSettings.AlwaysNotifyDiseaseIds.Select(d => new XtblUserDiseaseRelevance
                {
                    DiseaseId = d,
                    RelevanceId = (int) DiseaseRelevanceType.AlwaysNotify,
                    UserId = user.User.Id,
                    StateId = 1 // Default, in the future, need to keep existing state
                }));
                await _biodZebraContext.XtblUserDiseaseRelevance.AddRangeAsync(diseaseRelevanceSettings.RiskOnlyDiseaseIds.Select(d => new XtblUserDiseaseRelevance
                {
                    DiseaseId = d,
                    RelevanceId = (int) DiseaseRelevanceType.RiskOnly,
                    UserId = user.User.Id,
                    StateId = 1 // Default, in the future, need to keep existing state
                }));
                await _biodZebraContext.XtblUserDiseaseRelevance.AddRangeAsync(diseaseRelevanceSettings.NeverNotifyDiseaseIds.Select(d => new XtblUserDiseaseRelevance
                {
                    DiseaseId = d,
                    RelevanceId = (int) DiseaseRelevanceType.NeverNotify,
                    UserId = user.User.Id,
                    StateId = 1 // Default, in the future, need to keep existing state
                }));
            }
        }
    }
}