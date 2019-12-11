using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using static Biod.Solution.UnitTest.MockDbContext;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    class ZebraAnalyticsUserMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly string USER_1_ID = Guid.NewGuid().ToString();
        public static readonly DateTime? USER_1_FIRST_LOGIN_DATE = DateTime.Now.AddDays(-random.Next(1, 100));
        public static readonly DateTime USER_1_LAST_MODIFIED_DATE = DateTime.Now.AddDays(random.Next(1, 100));
        public static readonly string USER_NULL_FIELDS_ID = Guid.NewGuid().ToString();

        public static readonly MockApplicationUser USER_1 = new MockApplicationUser(new List<IdentityUserRole>()
        {
            new IdentityUserRole() { RoleId = random.Next(1, 1000000).ToString(), UserId = USER_1_ID },
            new IdentityUserRole() { RoleId = random.Next(1, 1000000).ToString(), UserId = USER_1_ID },
            new IdentityUserRole() { RoleId = random.Next(1, 1000000).ToString(), UserId = USER_1_ID }
        })
        {
            Id = USER_1_ID,
            FirstName = random.Next(1, 1000000).ToString(),
            LastName = random.Next(1, 1000000).ToString(),
            AoiGeonameIds = string.Join(",", new int[3]
            {
                random.Next(1, 100),
                random.Next(100, 200),
                random.Next(200, 300)
            }),
            Email = $"{random.Next(1000000, 9999999).ToString()}@bluedot.global",
            GeonameId = random.Next(1, 1000000),
            Location = random.Next(1, 1000000).ToString(),
            NewCaseNotificationEnabled = random.Next(1, 1000000) % 2 == 0,
            NewOutbreakNotificationEnabled = random.Next(1, 1000000) % 2 == 0,
            Organization = random.Next(1, 1000000).ToString(),
            PeriodicNotificationEnabled = random.Next(1, 1000000) % 2 == 0,
            UserGroupId = random.Next(1, 1000000),
            UserName = random.Next(1, 1000000).ToString(),
            WeeklyOutbreakNotificationEnabled = random.Next(1, 1000000) % 2 == 0
        };
        public static readonly ApplicationUser USER_NULL_FIELDS = new ApplicationUser()
        {
            Id = USER_NULL_FIELDS_ID
        };

        public Mock<UserManager<ApplicationUser>> MockContext { get; set; }

        public Mock<BiodZebraEntities> MockDbContext { get; set; }

        public ZebraAnalyticsUserMockDbSet()
        {
            MockContext = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            MockContext.Setup(context => context.Users).Returns(() => CreateMockedUsers());

            MockDbContext = new Mock<BiodZebraEntities>();
            MockDbContext.Setup(context => context.usp_GetFirstLoginDateByUser(It.IsAny<string>()))
                .Returns((string userId) => { return GetFirstLoginDateByUser(userId); });
            MockDbContext.Setup(context => context.usp_ZebraAnalyticsGetUserLastModifiedDate(It.IsAny<string>()))
                .Returns((string userId) => { return ZebraAnalyticsGetUserLastModifiedDate(userId); });
        }

        private ObjectResult<usp_GetFirstLoginDateByUser_Result> GetFirstLoginDateByUser(string userId)
        {
            var result = new Mock<TestableObjectResult<usp_GetFirstLoginDateByUser_Result>>();
            var resultList = new List<usp_GetFirstLoginDateByUser_Result>();

            if (userId == USER_1_ID)
            {
                resultList.Add(new usp_GetFirstLoginDateByUser_Result()
                {
                    Id = USER_1_ID,
                    UserName = USER_1.UserName,
                    FirstLoginDate = USER_1_FIRST_LOGIN_DATE
                });
            }
            else
            {
                resultList.Add(new usp_GetFirstLoginDateByUser_Result()
                {
                    Id = userId
                });
            }

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_GetFirstLoginDateByUser_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        private ObjectResult<usp_ZebraAnalyticsGetUserLastModifiedDate_Result> ZebraAnalyticsGetUserLastModifiedDate(string userId)
        {
            var result = new Mock<TestableObjectResult<usp_ZebraAnalyticsGetUserLastModifiedDate_Result>>();
            var resultList = new List<usp_ZebraAnalyticsGetUserLastModifiedDate_Result>();

            if (userId == USER_1_ID)
            {
                resultList.Add(new usp_ZebraAnalyticsGetUserLastModifiedDate_Result()
                {
                    UserId = USER_1_ID,
                    ModifiedDate = USER_1_LAST_MODIFIED_DATE
                });
            }

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraAnalyticsGetUserLastModifiedDate_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        private IQueryable<ApplicationUser> CreateMockedUsers()
        {
            return new List<ApplicationUser>()
            {
                USER_1,
                USER_NULL_FIELDS
            }.AsQueryable();
        }
    }
}
