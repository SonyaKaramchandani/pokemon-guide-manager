//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels.Zebra
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfile()
        {
            this.UserEmailNotifications = new HashSet<UserEmailNotification>();
            this.UserExternalIds = new HashSet<UserExternalId>();
            this.UserLoginTrans = new HashSet<UserLoginTran>();
            this.Xtbl_User_Disease_Relevance = new HashSet<Xtbl_User_Disease_Relevance>();
        }
    
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public Nullable<int> GeonameId { get; set; }
        public string AoiGeonameIds { get; set; }
        public string GridId { get; set; }
        public System.Guid UserTypeId { get; set; }
        public bool SmsNotificationEnabled { get; set; }
        public bool NewCaseNotificationEnabled { get; set; }
        public bool NewOutbreakNotificationEnabled { get; set; }
        public bool PeriodicNotificationEnabled { get; set; }
        public bool WeeklyOutbreakNotificationEnabled { get; set; }
        public bool DoNotTrackEnabled { get; set; }
        public bool OnboardingCompleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEmailNotification> UserEmailNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserExternalId> UserExternalIds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserLoginTran> UserLoginTrans { get; set; }
        public virtual UserType UserType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_User_Disease_Relevance> Xtbl_User_Disease_Relevance { get; set; }
    }
}
