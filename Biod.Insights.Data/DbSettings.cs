namespace Biod.Insights.Data
{
    /// <summary>
    /// Schema used in the App Settings JSON file that configures the database connection.
    /// </summary>
    /// <example>
    /// <code>
    /// "DbSettings": {
    ///    "EnableSensitiveDataLogging": true,
    ///    "MaxBatchSize": 200,
    ///    "MaxRetryCount": 3,
    ///    "RetryDelayMilliseconds": 500,
    ///    "CommandTimeout": 300
    /// }
    /// </code>
    /// </example>
    public class DbSettings
    {
        public bool EnableSensitiveDataLogging { get; set; }
        public int MaxBatchSize { get; set; }
        public int MaxRetryCount { get; set; }
        public int RetryDelayMilliseconds { get; set; }
        public int CommandTimeout { get; set; }
    }
}