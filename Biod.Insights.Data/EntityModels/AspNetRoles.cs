using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NotificationDescription { get; set; }
        public bool IsPublic { get; set; }

        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
