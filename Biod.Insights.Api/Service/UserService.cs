using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        
        /// <summary>
        /// User service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public UserService(
            BiodZebraContext biodZebraContext,
            ILogger<UserService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
        }

        public async Task<GetUserModel> GetUser(string userId)
        {
            var user = await _biodZebraContext.AspNetUsers
                .Include(u => u.AspNetUserRoles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {userId} does not exist");
            }
            
            return new GetUserModel
            {
                Id = user.Id,
                Roles = user.AspNetUserRoles.Select(ur => UserRoleService.ConvertToModel(ur.Role))
            };
        }
    }
}