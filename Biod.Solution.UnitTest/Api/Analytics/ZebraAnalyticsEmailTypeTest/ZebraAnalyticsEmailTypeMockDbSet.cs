using Biod.Zebra.Library.EntityModels;
using Moq;
using System;
using System.Collections.Generic;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    class ZebraAnalyticsEmailTypeMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly int EMAIL_TYPE_1_ID = random.Next(1000000, 9999999);
        public static readonly string EMAIL_TYPE_1_NAME = random.Next(1000000, 9999999).ToString();

        public Mock<BiodZebraEntities> MockContext { get; set; }

        public ZebraAnalyticsEmailTypeMockDbSet()
        {
            MockContext = new Mock<BiodZebraEntities>();
            MockContext.Setup(context => context.UserEmailTypes).ReturnsDbSet(CreateMockedEmailTypes());
        }

        private IEnumerable<UserEmailType> CreateMockedEmailTypes()
        {
            return new List<UserEmailType>()
            {
                new UserEmailType()
                {
                    Id = EMAIL_TYPE_1_ID,
                    Type = EMAIL_TYPE_1_NAME
                },
                new UserEmailType()
                {
                    Id = random.Next(1, 100),
                    Type = random.Next(1, 100000).ToString()
                },
                new UserEmailType()
                {
                    Id = random.Next(100, 200),
                    Type = random.Next(1, 100000).ToString()
                },
                new UserEmailType()
                {
                    Id = random.Next(200, 300),
                    Type = random.Next(1, 100000).ToString()
                },
                new UserEmailType()
                {
                    Id = random.Next(300, 400),
                    Type = random.Next(1, 100000).ToString()
                },
                new UserEmailType()
                {
                    Id = random.Next(400, 500),
                    Type = random.Next(1, 100000).ToString()
                },
                new UserEmailType()
                {
                    Id = random.Next(500, 600),
                    Type = random.Next(1, 100000).ToString()
                }
            };
        }
    }
}
