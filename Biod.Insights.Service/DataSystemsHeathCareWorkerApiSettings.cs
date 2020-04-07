using Biod.Insights.Common.HttpClients;

namespace Biod.Insights.Service
{
    public class DataSystemsHeathCareWorkerApiSettings : HttpSettings
    {
        public string NetworkUser { get; set; }
        public string NetworkPassword { get; set; }
    }
}