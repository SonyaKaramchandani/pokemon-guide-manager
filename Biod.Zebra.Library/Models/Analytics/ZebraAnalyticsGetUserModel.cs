using System;

namespace Biod.Zebra.Library.Models.Analytics
{
    public class ZebraAnalyticsGetUserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public int? GroupId { get; set; }

        public int[] AoiGeonameIds { get; set; }

        public string Location { get; set; }

        public int LocationGeonameId { get; set; }

        public DateTime? FirstLoginDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string[] Roles { get; set; }

        public string Organization { get; set; }

        public NotificationSettingViewModel[] NotificationSettings { get; set; }
    }
}
