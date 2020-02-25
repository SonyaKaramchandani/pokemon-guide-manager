using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
