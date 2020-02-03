using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biod.Insights.Api.Models
{
    public class ConnectionStrings
    {
        public string BiodZebraContext { get; set; }
    }
    public class DbSettings
    {
        public bool EnableSensitiveDataLogging { get; set; }
        public int MaxBatchSize { get; set; }
        public int MaxRetryCount { get; set; }
        public int RetryDelayMilliseconds { get; set; }
        public int CommandTimeout { get; set; }
    }
    public class JwtAuth
    {
        public bool RequiresHttps { get; set; }
        public string SecurityKey { get; set; }
    }
    public class HttpSettings
    {
        public string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
        public int MaxRetryCount { get; set; }
        public int RetryDelaySeconds { get; set; }
        public int JitterMilliseconds { get; set; }
        public int HandlerLifetimeMinutes { get; set; }
    }
    public class GeorgeApiSettings: HttpSettings
    {
        public string NetworkUser { get; set; }
        public string NetworkPassword { get; set; }
    }
    /// <summary>
    /// App settings JSON file mapping class
    /// </summary>
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string AllowedHosts { get; set; }
        public DbSettings DbSettings { get; set; }
        public JwtAuth JwtAuth { get; set; }
        public GeorgeApiSettings GeorgeApi { get; set; }
    }
}
