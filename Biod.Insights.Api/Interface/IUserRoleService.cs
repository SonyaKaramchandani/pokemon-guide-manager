using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Models.User;

namespace Biod.Insights.Api.Interface
{
    public interface IUserRoleService
    {
        Task<UserRoleModel> GetUserRole(string roleId);
        
        Task<IEnumerable<UserRoleModel>> GetUserRoles();
        
        Task<IEnumerable<UserRoleModel>> GetPublicUserRoles();
        
        Task<IEnumerable<UserRoleModel>> GetPrivateUserRoles();
    }
}