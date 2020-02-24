using System.Collections.Generic;

namespace Biod.Insights.Notification.Engine.Services.EmailDelivery
{
    public class EmailMessage
    {
        public List<string> To { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
