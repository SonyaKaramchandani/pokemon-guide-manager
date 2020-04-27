using System;
using Biod.Products.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biod.Insights.Data
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
        public static IServiceCollection AddDataDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(GlobalVariables.AppSettingsSection.DATABASE_SETTINGS).Get<DbSettings>();
            if(dbSettings==null)
            {
                throw new Exception($"The section '{GlobalVariables.AppSettingsSection.DATABASE_SETTINGS}' settings section is missing!"); //TODO: use custom exception
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