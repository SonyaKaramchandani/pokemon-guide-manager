using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ILogger<UserRoleService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;

        /// <summary>
        /// User role service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseRelevanceService">the disease relevance service</param>
        public UserRoleService(
            BiodZebraContext biodZebraContext,
            ILogger<UserRoleService> logger,
            IDiseaseRelevanceService diseaseRelevanceService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseRelevanceService = diseaseRelevanceService;
        }


        public async Task<UserRoleModel> GetUserRole(string roleId)
        {
            var config = new UserRoleConfig.Builder()
                .SetRoleId(roleId)
                .ShouldIncludePublicRolesOnly()
                .Build();

            return ConvertToModel((await new UserRoleQueryBuilder(_biodZebraContext, config).BuildAndExecute()).First());
        }

        public async Task<IEnumerable<UserRoleModel>> GetUserRoles()
        {
            return (await new UserRoleQueryBuilder(_biodZebraContext).BuildAndExecute()).Select(ConvertToModel);
        }

        public async Task<IEnumerable<UserRoleModel>> GetPublicUserRoles()
        {
            var result = new List<UserRoleModel>();
            foreach (var userType in await _biodZebraContext.UserTypes.ToListAsync())
            {
                result.Add(new UserRoleModel
                {
                    Id = userType.Id.ToString(),
                    Name = userType.Name,
                    IsPublic = true,
                    RelevanceSettings = await _diseaseRelevanceService.GetUserTypeDiseaseRelevanceSettings(userType.Id)
                });
            }

            return result;
        }

        public async Task<IEnumerable<UserRoleModel>> GetPrivateUserRoles()
        {
            var config = new UserRoleConfig.Builder()
                .ShouldIncludePrivateRolesOnly()
                .Build();

            return (await new UserRoleQueryBuilder(_biodZebraContext, config).BuildAndExecute()).Select(ConvertToModel);
        }

        public static UserRoleModel ConvertToModel(AspNetRoles role)
        {
            return new UserRoleModel
            {
                Id = role.Id,
                Name = role.Name,
                IsPublic = role.IsPublic
            };
        }
    }
}