using Biod.Insights.Common.HttpClients;
using Biod.Insights.Data;
using Biod.Insights.Service;

namespace Biod.Insights.Api
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
