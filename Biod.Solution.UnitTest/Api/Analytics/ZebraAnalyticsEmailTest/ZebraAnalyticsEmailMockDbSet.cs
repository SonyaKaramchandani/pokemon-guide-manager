using Biod.Zebra.Library.EntityModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using static Biod.Solution.UnitTest.MockDbContext;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    class ZebraAnalyticsEmailMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly int EMAIL_1_ID = random.Next(1000000, 9999999);
        public static readonly string EMAIL_1_CONTENT = random.Next(1000000, 9999999).ToString();
        public static readonly int EMAIL_NULL_CONTENT_ID = random.Next(1000000, 9999999);

        public static readonly string SINGLE_EMAIL_USER_ID = Guid.NewGuid().ToString();
        public static readonly DateTimeOffset SINGLE_EMAIL_SENT_DATE = DateTimeOffset.UtcNow.AddDays(random.Next(1, 100));
        public static readonly usp_ZebraAnalyticsGetUserEmailNotification_Result SINGLE_EMAIL = new usp_ZebraAnalyticsGetUserEmailNotification_Result()
        {
            Id = random.Next(1000000, 9999999),
            EmailType = random.Next(1000000, 9999999),
            EventId = random.Next(1000000, 9999999),
            UserId = SINGLE_EMAIL_USER_ID,
            SentDate = SINGLE_EMAIL_SENT_DATE,
            AoiGeonameIds = random.Next(1000000, 9999999).ToString(),
            UserEmail = random.Next(1000000, 9999999).ToString()
        };

        public static readonly string MULTIPLE_EMAIL_USER_ID = Guid.NewGuid().ToString();
        public static readonly string MULTIPLE_EMAIL_USER_EMAIL = Guid.NewGuid().ToString();
        public static readonly List<usp_ZebraAnalyticsGetUserEmailNotification_Result> MULTIPLE_EMAILS = new List<usp_ZebraAnalyticsGetUserEmailNotification_Result>()
        {
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 1,
                EmailType = 1,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2018, 12, 31, 0, 0, 0, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 2,
                EmailType = 1,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2018, 12, 31, 23, 59, 59, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 3,
                EmailType = 2,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 1, 1, 0, 0, 0, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 4,
                EmailType = 1,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 1, 1, 23, 59, 59, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 5,
                EmailType = 2,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 1, 31, 0, 0, 0, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 6,
                EmailType = 3,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 1, 31, 23, 59, 59, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 7,
                EmailType = 1,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 12, 1, 0, 0, 0, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 8,
                EmailType = 2,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 12, 1, 23, 59, 59, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 9,
                EmailType = 3,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 12, 31, 0, 0, 0, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 10,
                EmailType = 4,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2019, 12, 31, 23, 59, 59, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
            new usp_ZebraAnalyticsGetUserEmailNotification_Result()
            {
                Id = 11,
                EmailType = 1,
                UserEmail = MULTIPLE_EMAIL_USER_EMAIL,
                SentDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, 0, TimeSpan.Zero),
                UserId = MULTIPLE_EMAIL_USER_ID,
                EventId = null
            },
        };


        public Mock<BiodZebraEntities> MockContext { get; set; }

        public ZebraAnalyticsEmailMockDbSet()
        {
            MockContext = new Mock<BiodZebraEntities>();
            MockContext.Setup(context => context.UserEmailNotifications).ReturnsDbSet(CreateMockedEmails());

            MockContext.Setup(context => context.usp_ZebraAnalyticsGetUserEmailNotification(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<DateTimeOffset?>(),
                It.IsAny<DateTimeOffset?>()))
                .Returns((
                    string userId,
                    string email,
                    int? emailType,
                    DateTimeOffset? startDate,
                    DateTimeOffset? endDate
                ) =>
                { return ZebraAnalyticsGetUserEmailNotification(userId, email, emailType, startDate, endDate); });
        }

        private ObjectResult<usp_ZebraAnalyticsGetUserEmailNotification_Result> ZebraAnalyticsGetUserEmailNotification(
            string userId,
            string email,
            int? emailType,
            DateTimeOffset? startDate,
            DateTimeOffset? endDate)
        {
            var result = new Mock<TestableObjectResult<usp_ZebraAnalyticsGetUserEmailNotification_Result>>();
            var resultList = new List<usp_ZebraAnalyticsGetUserEmailNotification_Result>();

            if (userId == SINGLE_EMAIL_USER_ID)
            {
                resultList.Add(SINGLE_EMAIL);
            }
            else if (userId == MULTIPLE_EMAIL_USER_ID)
            {
                resultList.AddRange(MULTIPLE_EMAILS);
            }

            resultList = resultList
                .Where(r =>
                    (startDate == null || r.SentDate >= startDate.Value.UtcDateTime)
                    && (endDate == null || r.SentDate < endDate.Value.UtcDateTime)
                    && (emailType == null || r.EmailType == emailType)
                    && (email == null || r.UserEmail == email)
                ).ToList();

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraAnalyticsGetUserEmailNotification_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        private IEnumerable<UserEmailNotification> CreateMockedEmails()
        {
            return new List<UserEmailNotification>()
            {
                new UserEmailNotification()
                {
                    Id = EMAIL_1_ID,
                    Content = EMAIL_1_CONTENT
                },
                new UserEmailNotification()
                {
                    Id = EMAIL_NULL_CONTENT_ID
                }
            };
        }
    }
}
