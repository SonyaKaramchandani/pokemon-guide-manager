using Newtonsoft.Json;

namespace Biod.Insights.Notification.Engine.Models
{
    public class EmailRenderingModel
    {
        public int Type { get; set; }
        
        public EmailViewModel Data { get; set; }
    }
}