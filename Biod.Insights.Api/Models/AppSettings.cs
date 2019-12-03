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
    /// <summary>
    /// App settings JSON file mapping class
    /// </summary>
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string AllowedHosts { get; set; }
        public DbSettings DbSettings { get; set; }
    }
}
