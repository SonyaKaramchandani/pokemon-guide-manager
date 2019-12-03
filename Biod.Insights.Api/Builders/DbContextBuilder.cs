using Biod.Insights.Api.Data.Models;
using Biod.Insights.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Biod.Insights.Api.Builders
{
    /// <summary>
    /// Service builder extension
    /// </summary>
    public static class DbContextBuilder
    {
        /// <summary>
        /// Configure DB contexts required for the Api
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private const string DbSetting_ConfigSection = "DbSettings";
        public static IServiceCollection AddApiDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(DbSetting_ConfigSection).Get<DbSettings>();
            if(dbSettings==null)
            {
                throw new Exception($"The section '{DbSetting_ConfigSection}' settings section is missing!"); //TODO: use custom exception
            }

            services.AddDbContext<BiodZebraContext>(options =>
            {
                options.EnableSensitiveDataLogging(dbSettings.EnableSensitiveDataLogging);
                options.UseSqlServer(configuration.GetConnectionString("BiodZebraContext"),
                    sql =>
                    {
                        sql.UseNetTopologySuite();
                        sql.MaxBatchSize(dbSettings.MaxBatchSize);
                        sql.EnableRetryOnFailure(
                                dbSettings.MaxRetryCount,
                                TimeSpan.FromMilliseconds(dbSettings.RetryDelayMilliseconds),
                                errorNumbersToAdd: null);
                        sql.CommandTimeout(dbSettings.CommandTimeout);
                    });
            });
            return services;
        }
    }
}