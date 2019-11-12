using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Moq;
using System;
using System.Linq;

namespace Biod.Solution.UnitTest.Api.Surveillance.ZebraNotificationsTest
{
    public abstract class NotificationTest
    {
        public static readonly Random random = new Random();

        public Mock<BiodZebraEntities> mockDbContext;
        public Mock<UserManager<ApplicationUser>> mockUserManager;

        public virtual void Initialize()
        {
            var dbMock = new ZebraNotificationMockDbSet();
            mockDbContext = dbMock.MockContext;
        }

        public ApplicationUser CreateMockUser()
        {
            var email = GenerateRandomString(6) + "@bluedot.global";
            return new ApplicationUser
            {
                AoiGeonameIds = "6167865",
                DoNotTrackEnabled = false,
                Email = email,
                EmailConfirmed = true,
                FirstName = GenerateRandomString(5),
                GeonameId = 6167865,
                GridId = "IC-85",
                Id = GenerateRandomString(36),
                LastName = GenerateRandomString(5),
                Location = "Toronto, Ontario, Canada",
                LockoutEnabled = true,
                LockoutEndDateUtc = null,
                NewCaseNotificationEnabled = true,
                NewOutbreakNotificationEnabled = true,
                OnboardingCompleted = true,
                Organization = GenerateRandomString(10),
                PasswordHash = GenerateRandomString(70),
                PeriodicNotificationEnabled = true,
                PhoneNumber = random.Next(100000000, 999999999).ToString(),
                PhoneNumberConfirmed = false,
                SecurityStamp = GenerateRandomString(35),
                SmsNotificationEnabled = false,
                TwoFactorEnabled = false,
                UserGroupId = null,
                UserName = email,
                WeeklyOutbreakNotificationEnabled = true
            };
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
