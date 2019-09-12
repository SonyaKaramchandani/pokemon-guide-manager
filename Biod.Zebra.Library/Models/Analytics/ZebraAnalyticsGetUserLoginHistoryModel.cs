using System;

namespace Biod.Zebra.Library.Models.Analytics
{
    public class ZebraAnalyticsGetUserLoginHistoryModel
    {
        public string UserId { get; set; }

        public DateTime LoginDate { get; set; }
    }
}
