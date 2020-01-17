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
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;

        /// <summary>
        /// User service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseRelevanceService">the disease relevance service</param>
        public UserService(
            BiodZebraContext biodZebraContext,
            ILogger<UserService> logger,
            IDiseaseRelevanceService diseaseRelevanceService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseRelevanceService = diseaseRelevanceService;
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
            
            var result = new GetUserModel
            {
                Id = user.Id,
                Roles = user.AspNetUserRoles.Select(ur => UserRoleService.ConvertToModel(ur.Role)),
                IsDoNotTrack = user.DoNotTrackEnabled
            };
            result.DiseaseRelevanceSetting = await _diseaseRelevanceService.GetUserDiseaseRelevanceSettings(result);

            return result;
        }
    }
}