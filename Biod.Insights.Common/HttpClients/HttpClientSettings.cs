namespace Biod.Insights.Common.HttpClients
{
    public class HttpSettings
    {
        public string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
        public int MaxRetryCount { get; set; }
        public int RetryDelaySeconds { get; set; }
        public int JitterMilliseconds { get; set; }
        public int HandlerLifetimeMinutes { get; set; }
    }
}