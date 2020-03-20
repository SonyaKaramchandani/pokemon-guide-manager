namespace Biod.Insights.Service.Models.User
{
    public class UserNotificationsModel
    {
        public bool IsEventEmailEnabled { get; set; }
        
        public bool IsWeeklyEmailEnabled { get; set; }
        
        public bool IsProximalEmailEnabled { get; set; }
    }
}