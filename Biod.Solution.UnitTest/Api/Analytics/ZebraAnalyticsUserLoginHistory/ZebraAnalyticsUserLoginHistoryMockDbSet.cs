using Biod.Zebra.Library.EntityModels.Zebra;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using static Biod.Solution.UnitTest.MockDbContext;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    class ZebraAnalyticsUserLoginHistoryMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly string SINGLE_LOGIN_HISTORY_USER_ID = Guid.NewGuid().ToString();
        public static readonly DateTimeOffset SINGLE_LOGIN_HISTORY_LOGIN_DATE = DateTimeOffset.UtcNow.AddDays(random.Next(1, 300));

        public static readonly string MULTIPLE_LOGIN_HISTORY_USER_ID = Guid.NewGuid().ToString();

        public static readonly List<DateTimeOffset> MULTIPLE_LOGIN_HISTORY_LOGIN_DATES = new List<DateTimeOffset>()
        {
            new DateTimeOffset(2018, 12, 31, 0, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2018, 12, 31, 23, 59, 59, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 1, 1, 0, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 1, 1, 23, 59, 59, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 1, 31, 0, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 1, 31, 23, 59, 59, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 12, 1, 0, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 12, 1, 23, 59, 59, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 12, 31, 0, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2019, 12, 31, 23, 59, 59, 0, TimeSpan.Zero),
            new DateTimeOffset(2020, 1, 1, 0, 0, 0, 0, TimeSpan.Zero)
        };

        public Mock<BiodZebraEntities> MockContext { get; set; }

        public ZebraAnalyticsUserLoginHistoryMockDbSet()
        {
            MockContext = new Mock<BiodZebraEntities>();
            MockContext.Setup(context => context.usp_ZebraAnalyticsGetUserLogin(It.IsAny<string>(), It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>()))
                .Returns((string userId, DateTimeOffset? startDate, DateTimeOffset? endDate) => { return ZebraAnalyticsGetUserLogin(userId, startDate, endDate); });
        }

        private ObjectResult<usp_ZebraAnalyticsGetUserLogin_Result> ZebraAnalyticsGetUserLogin(string userId, DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            var result = new Mock<TestableObjectResult<usp_ZebraAnalyticsGetUserLogin_Result>>();
            var resultList = new List<usp_ZebraAnalyticsGetUserLogin_Result>();

            if (userId == SINGLE_LOGIN_HISTORY_USER_ID)
            {
                resultList.Add(new usp_ZebraAnalyticsGetUserLogin_Result()
                {
                    UserId = SINGLE_LOGIN_HISTORY_USER_ID,
                    LoginDateTime = SINGLE_LOGIN_HISTORY_LOGIN_DATE.UtcDateTime
                });
            }
            else if (userId == MULTIPLE_LOGIN_HISTORY_USER_ID)
            {
                resultList.AddRange(MULTIPLE_LOGIN_HISTORY_LOGIN_DATES.Select(d => new usp_ZebraAnalyticsGetUserLogin_Result()
                {
                    UserId = MULTIPLE_LOGIN_HISTORY_USER_ID,
                    LoginDateTime = d.UtcDateTime
                }));
            }

            resultList = resultList
                .Where(r =>
                    (startDate == null || r.LoginDateTime >= startDate.Value.UtcDateTime)
                    && (endDate == null || r.LoginDateTime < endDate.Value.UtcDateTime)
                ).ToList();

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraAnalyticsGetUserLogin_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }
    }
}
