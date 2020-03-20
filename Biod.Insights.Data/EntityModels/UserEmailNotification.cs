using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class UserEmailNotification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AoiGeonameIds { get; set; }
        public string UserEmail { get; set; }
        public int EmailType { get; set; }
        public int? EventId { get; set; }
        public string Content { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }

        public virtual Event Event { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
