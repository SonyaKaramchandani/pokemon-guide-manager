using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class UserEmailType
    {
        public UserEmailType()
        {
            UserEmailNotification = new HashSet<UserEmailNotification>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<UserEmailNotification> UserEmailNotification { get; set; }
    }
}
