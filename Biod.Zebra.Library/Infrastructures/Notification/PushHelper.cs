using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Library.Models.Notification.Push;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    class PushHelper
    {
        private readonly string _fcmBaseUrl = ConfigurationManager.AppSettings.Get("FcmBaseUrl");
        private readonly string _fcmServerKey = ConfigurationManager.AppSettings.Get("FcmServerKey");
        private readonly string _fcmSenderKey = ConfigurationManager.AppSettings.Get("FcmSenderKey");

        private readonly ILogger _logger = Logger.GetLogger(typeof(PushHelper).ToString());

        public async Task SendZebraPushNotifications(IPushViewModel notification)
        {
            string emailType;
            switch (notification.NotificationType)
            {
                case Constants.EmailTypes.EVENT_EMAIL:
                    emailType = "NEW_OUTBREAK";
                    break;
                case Constants.EmailTypes.PROXIMAL_EMAIL:
                    emailType = "PROXIMAL_CASE";
                    break;
                case Constants.EmailTypes.WEEKLY_BRIEF_EMAIL:
                    emailType = "WEEKLY_BRIEF";
                    break;
                default:
                    _logger.Warning($"Unknown email type: {notification.NotificationType}");
                    return;
            }

            if (notification.DeviceTokenList != null)
            {
                await Task.WhenAll(notification.DeviceTokenList.Select(token => SendZebraPushNotification(token, emailType, notification)).ToArray());
            }
        }

        public async Task<HttpResponseMessage> SendZebraPushNotification(string token, string emailType, IPushViewModel notification)
        {
            var payload = new
            {
                to = token,
                data = new
                {
                    id = notification.NotificationId,
                    type = emailType,
                    title = notification.Title,
                    summary = notification.Summary ?? ""
                },
                priority = "normal"
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_fcmBaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_fcmServerKey}");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={_fcmSenderKey}");

                var json = JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync("/fcm/send", httpContent);
                
                if (!result.IsSuccessStatusCode)
                {
                    _logger.Warning($"Failed to push notification id {notification.NotificationId} to user {notification.UserId}");
                }
                else
                {
                    _logger.Info($"Successfully pushed notification id {notification.NotificationId} to user {notification.UserId}");
                }

                return result;
            }
        }
    }
}