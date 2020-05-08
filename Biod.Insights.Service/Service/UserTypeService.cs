using System;
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
    public class UserTypeService : IUserTypeService
    {
        private readonly ILogger<UserTypeService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IDiseaseRelevanceService _diseaseRelevanceService;

        /// <summary>
        /// User role service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="diseaseRelevanceService">the disease relevance service</param>
        public UserTypeService(
            BiodZebraContext biodZebraContext,
            ILogger<UserTypeService> logger,
            IDiseaseRelevanceService diseaseRelevanceService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _diseaseRelevanceService = diseaseRelevanceService;
        }


        public async Task<UserTypeModel> GetUserType(Guid userTypeId)
        {
            var config = new UserTypeConfig.Builder()
                .SetUserTypeId(userTypeId)
                .Build();

            return ConvertToModel((await new UserTypeQueryBuilder(_biodZebraContext, config).BuildAndExecute()).First());
        }

        public async Task<IEnumerable<UserTypeModel>> GetUserTypes()
        {
            var result = new List<UserTypeModel>();
            foreach (var userType in await _biodZebraContext.UserTypes.ToListAsync())
            {
                result.Add(new UserTypeModel
                {
                    Id = userType.Id,
                    Name = userType.Name,
                    RelevanceSettings = await _diseaseRelevanceService.GetUserTypeDiseaseRelevanceSettings(userType.Id)
                });
            }

            return result;
        }

        public static UserTypeModel ConvertToModel(UserTypes role)
        {
            return new UserTypeModel
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}