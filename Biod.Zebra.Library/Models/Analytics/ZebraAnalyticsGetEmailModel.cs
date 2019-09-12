using System;

namespace Biod.Zebra.Library.Models.Analytics
{
    public class ZebraAnalyticsGetEmailModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public int EmailType { get; set; }

        public DateTimeOffset SentDate { get; set; }

        public string AoiGeonameIds { get; set; }

        public int? EventId { get; set; }
    }
}
