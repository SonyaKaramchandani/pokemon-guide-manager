namespace Biod.Insights.Notification.Engine
{
    public class NotificationSettings
    {
        /// <summary>
        /// Base url for the Insights App, used for constructing URLs for the email
        /// </summary>
        public string InsightsAppBaseUrl { get; set; }
        
        /// <summary>
        /// Base url for the Insights App, used for constructing URLs for links pointing to user settings
        /// </summary>
        public string ZebraAppBaseUrl { get; set; }
        
        /// <summary>
        /// Flag whether analytics is enabled for emails
        /// </summary>
        public bool IsAnalyticsEnabled { get; set; }

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
    }
}