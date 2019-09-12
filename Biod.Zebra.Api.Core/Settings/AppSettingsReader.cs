using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biod.Zebra.Api.Core.Settings
{
    /// <summary>
    /// App Settings config reader
    /// </summary>
    public static class AppSettingsReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsReader" /> class.
        /// </summary>
        public static AppSettings GetAppSettings()
        {
            //object graph configuration
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var appConfig = new AppSettings();
            config.GetSection("AppSettings").Bind(appConfig);

            return appConfig;
        }
    }
}
