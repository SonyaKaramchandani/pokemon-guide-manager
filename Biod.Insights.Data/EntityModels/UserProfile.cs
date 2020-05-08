using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            HcwCases = new HashSet<HcwCases>();
            UserEmailNotification = new HashSet<UserEmailNotification>();
            XtblUserDiseaseRelevance = new HashSet<XtblUserDiseaseRelevance>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int? GeonameId { get; set; }
        public string AoiGeonameIds { get; set; }
        public string GridId { get; set; }
        public Guid UserTypeId { get; set; }
        public bool SmsNotificationEnabled { get; set; }
        public bool? NewCaseNotificationEnabled { get; set; }
        public bool? NewOutbreakNotificationEnabled { get; set; }
        public bool? PeriodicNotificationEnabled { get; set; }
        public bool? WeeklyOutbreakNotificationEnabled { get; set; }
        public bool DoNotTrackEnabled { get; set; }
        public bool OnboardingCompleted { get; set; }

        public virtual UserTypes UserType { get; set; }
        public virtual ICollection<HcwCases> HcwCases { get; set; }
        public virtual ICollection<UserEmailNotification> UserEmailNotification { get; set; }
        public virtual ICollection<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
    }
}
