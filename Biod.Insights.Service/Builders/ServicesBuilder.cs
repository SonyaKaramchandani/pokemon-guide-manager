using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Biod.Insights.Service.Builders
{
    public static class ServicesBuilder
    {
        /// <summary>
        /// Configure the services required for the Api
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<ICaseCountService, CaseCountService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<IDiseaseRelevanceService, DiseaseRelevanceService>();
            services.AddScoped<IDiseaseRiskService, DiseaseRiskService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGeonameService, GeonameService>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IOutbreakPotentialService, OutbreakPotentialService>();
            services.AddScoped<IRiskCalculationService, RiskCalculationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLocationService, UserLocationService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            return services;
        }
    }
}