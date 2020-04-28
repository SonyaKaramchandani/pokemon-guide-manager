using System;
using System.Linq;
using Biod.Products.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class AppMetadataService : IAppMetadataService
    {
        private readonly ILogger<AppMetadataService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        public AppMetadataService(ILogger<AppMetadataService> logger, BiodZebraContext biodZebraContext)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
        }

        public ApplicationMetadataModel GetMetadata()
        {
            return new ApplicationMetadataModel
            {
                LandscanDatasetYear = SqlQuery.GetConfigurationVariable(_biodZebraContext, nameof(ConfigurationVariableName.LandscanDataYear), DateTime.Now.Year - 2),
                IataDatasetYear = SqlQuery.GetConfigurationVariable(_biodZebraContext, nameof(ConfigurationVariableName.IataDataYear), DateTime.Now.Year - 1),
                InnovataDatasetYear = SqlQuery.GetConfigurationVariable(_biodZebraContext, nameof(ConfigurationVariableName.InnovataDataYear), DateTime.Now.Year)
            };
        }
    }
}