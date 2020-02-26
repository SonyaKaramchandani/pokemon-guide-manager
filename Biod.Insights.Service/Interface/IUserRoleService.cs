using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Service.Models.User;

namespace Biod.Insights.Service.Interface
{
    public interface IUserRoleService
    {
        Task<UserRoleModel> GetUserRole(string roleId);
        
        Task<IEnumerable<UserRoleModel>> GetUserRoles();
        
        Task<IEnumerable<UserRoleModel>> GetPublicUserRoles();
        
        Task<IEnumerable<UserRoleModel>> GetPrivateUserRoles();
    }
}