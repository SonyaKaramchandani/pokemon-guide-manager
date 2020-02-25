using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.User;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
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
            return ConvertToModel((await new UserRoleQueryBuilder(_biodZebraContext)
                    .SetRoleId(roleId)
                    .IncludePublicRolesOnly()
                    .BuildAndExecute())
                .First());
        }

        public async Task<IEnumerable<UserRoleModel>> GetUserRoles()
        {
            return (await new UserRoleQueryBuilder(_biodZebraContext)
                    .BuildAndExecute())
                .Select(ConvertToModel);
        }

        public async Task<IEnumerable<UserRoleModel>> GetPublicUserRoles()
        {
            return (await new UserRoleQueryBuilder(_biodZebraContext)
                    .IncludePublicRolesOnly()
                    .BuildAndExecute())
                .Select(ConvertToModel);
        }

        public async Task<IEnumerable<UserRoleModel>> GetPrivateUserRoles()
        {
            return (await new UserRoleQueryBuilder(_biodZebraContext)
                    .IncludePrivateRolesOnly()
                    .BuildAndExecute())
                .Select(ConvertToModel);
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