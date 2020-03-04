using Biod.Insights.Common.HttpClients;

namespace Biod.Insights.Notification.Engine
{
    public class NotificationSettings
    {
        /// <summary>
        /// Number of locations to show in Weekly Emails
        /// </summary>
        public int WeeklyEmailTopLocations { get; set; } = 3;
        
        /// <summary>
        /// Number of events to show per location in Weekly Emails
        /// </summary>
        public int WeeklyEmailTopEvents { get; set; } = 3;
        
        /// <summary>
        /// SendGrid Api Key required for invoking SendGrid Send web api
        /// </summary>
        public string SendGridApiKey { get; set; }

        /// <summary>
        /// Email address of email sender, used as from email address in emails sent out
        /// </summary>
        public string EmailSenderAddress { get; set; }

        /// <summary>
        /// Name of email sender, used as from name in emails sent out
        /// </summary>
        public string EmailSenderName { get; set; }

        /// <summary>
        /// Http Settings for the HttpClient when communicating to the Email rendering service
        /// </summary>
        public HttpSettings EmailRenderingApiSettings { get; set; }
    }
}