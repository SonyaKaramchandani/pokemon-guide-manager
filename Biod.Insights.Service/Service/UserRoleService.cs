using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.User;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ILogger<UserRoleService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// User role service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public UserRoleService(
            BiodZebraContext biodZebraContext,
            ILogger<UserRoleService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
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
            var config = new UserRoleConfig.Builder()
                .ShouldIncludePublicRolesOnly()
                .Build();
            
            return (await new UserRoleQueryBuilder(_biodZebraContext, config).BuildAndExecute()).Select(ConvertToModel);
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