using Biod.Products.Common.HttpClients;

namespace Biod.Insights.Notification.Engine
{
    public class NotificationSettings
    {
        /// <summary>
        /// Flag determining whether emails are under testing (not sent to the real user)
        /// </summary>
        public bool EnableTestingMode { get; set; } = true;

        /// <summary>
        /// List of emails that would receive the email instead of the intended user when in testing mode
        /// </summary>
        public string TestingRecipientList { get; set; } = "InsightsQA@bluedot.global";
        
        /// <summary>
        /// Number of locations to show in Weekly Emails
        /// </summary>
        public int WeeklyEmailTopLocations { get; set; } = 3;
        
        /// <summary>
        /// Number of events to show per location in Weekly Emails
        /// </summary>
        public int WeeklyEmailTopEvents { get; set; } = 3;
        
        /// <summary>
        /// Number of locations to show in New Event Emails
        /// </summary>
        public int EventEmailTopLocations { get; set; } = 3;
        
        /// <summary>
        /// Threshold for number of preceding days for a disease activity to be considered recent
        /// </summary>
        public int ProximalEmailRecentThresholdInDays { get; set; } = 7;
        
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