using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class UserTypes
    {
        public UserTypes()
        {
            UserProfile = new HashSet<UserProfile>();
            UserTypeDiseaseRelevances = new HashSet<UserTypeDiseaseRelevances>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NotificationDescription { get; set; }

        public virtual ICollection<UserProfile> UserProfile { get; set; }
        public virtual ICollection<UserTypeDiseaseRelevances> UserTypeDiseaseRelevances { get; set; }
    }
}
