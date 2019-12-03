using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class UserTransLog
    {
        public string UserId { get; set; }
        public DateTime ModifiedUtcdatetime { get; set; }
        public string ModificationDescription { get; set; }
    }
}
