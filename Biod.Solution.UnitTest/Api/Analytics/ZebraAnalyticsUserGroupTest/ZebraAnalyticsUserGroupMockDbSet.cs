using Biod.Zebra.Library.EntityModels;
using Moq;
using System;
using System.Collections.Generic;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    class ZebraAnalyticsUserGroupMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly int USER_GROUP_1_ID = random.Next(1000000, 9999999);
        public static readonly string USER_GROUP_1_NAME = random.Next(1000000, 9999999).ToString();

        public Mock<BiodZebraEntities> MockContext { get; set; }

        public ZebraAnalyticsUserGroupMockDbSet()
        {
            MockContext = new Mock<BiodZebraEntities>();
            MockContext.Setup(context => context.UserGroups).ReturnsDbSet(CreateMockedUserGroups());
        }

        private IEnumerable<UserGroup> CreateMockedUserGroups()
        {
            return new List<UserGroup>()
            {
                new UserGroup()
                {
                    Id = USER_GROUP_1_ID,
                    Name = USER_GROUP_1_NAME
                },
                new UserGroup()
                {
                    Id = random.Next(1, 100),
                    Name = random.Next(1, 100000).ToString()
                },
                new UserGroup()
                {
                    Id = random.Next(100, 200),
                    Name = random.Next(1, 100000).ToString()
                },
                new UserGroup()
                {
                    Id = random.Next(200, 300),
                    Name = random.Next(1, 100000).ToString()
                },
                new UserGroup()
                {
                    Id = random.Next(300, 400),
                    Name = random.Next(1, 100000).ToString()
                },
                new UserGroup()
                {
                    Id = random.Next(400, 500),
                    Name = random.Next(1, 100000).ToString()
                },
                new UserGroup()
                {
                    Id = random.Next(500, 600),
                    Name = random.Next(1, 100000).ToString()
                }
            };
        }
    }
}
