using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class UserAoisHistory
    {
        public string UserId { get; set; }
        public string AoiGeonameIds { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
