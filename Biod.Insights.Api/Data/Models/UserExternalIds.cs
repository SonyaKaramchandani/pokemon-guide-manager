using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class UserExternalIds
    {
        public string ExternalName { get; set; }
        public string ExternalId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset LastCommunicationDate { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
