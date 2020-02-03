using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            EventImportationRisksByUser = new HashSet<EventImportationRisksByUser>();
            EventImportationRisksByUserHistory = new HashSet<EventImportationRisksByUserHistory>();
            UserEmailNotification = new HashSet<UserEmailNotification>();
            UserExternalIds = new HashSet<UserExternalIds>();
            UserLoginTrans = new HashSet<UserLoginTrans>();
            XtblUserDiseaseRelevance = new HashSet<XtblUserDiseaseRelevance>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Location { get; set; }
        public int GeonameId { get; set; }
        public string AoiGeonameIds { get; set; }
        public string GridId { get; set; }
        public bool SmsNotificationEnabled { get; set; }
        public bool? NewCaseNotificationEnabled { get; set; }
        public bool? NewOutbreakNotificationEnabled { get; set; }
        public bool? PeriodicNotificationEnabled { get; set; }
        public bool? WeeklyOutbreakNotificationEnabled { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public int? UserGroupId { get; set; }
        public bool DoNotTrackEnabled { get; set; }
        public bool OnboardingCompleted { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenCreatedDate { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        public virtual UserAoisHistory UserAoisHistory { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<EventImportationRisksByUser> EventImportationRisksByUser { get; set; }
        public virtual ICollection<EventImportationRisksByUserHistory> EventImportationRisksByUserHistory { get; set; }
        public virtual ICollection<UserEmailNotification> UserEmailNotification { get; set; }
        public virtual ICollection<UserExternalIds> UserExternalIds { get; set; }
        public virtual ICollection<UserLoginTrans> UserLoginTrans { get; set; }
        public virtual ICollection<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
    }
}
