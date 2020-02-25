using Biod.Insights.Common.HttpClients;
using Biod.Insights.Data;

namespace Biod.Insights.Api.Models
{
    public class ConnectionStrings
    {
        public string BiodZebraContext { get; set; }
    }

    public class JwtAuth
    {
        public bool RequiresHttps { get; set; }
        public string SecurityKey { get; set; }
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
