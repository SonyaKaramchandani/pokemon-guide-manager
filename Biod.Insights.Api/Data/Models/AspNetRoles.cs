using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            XtblRoleDiseaseRelevance = new HashSet<XtblRoleDiseaseRelevance>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NotificationDescription { get; set; }
        public bool IsPublic { get; set; }

        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<XtblRoleDiseaseRelevance> XtblRoleDiseaseRelevance { get; set; }
    }
}
