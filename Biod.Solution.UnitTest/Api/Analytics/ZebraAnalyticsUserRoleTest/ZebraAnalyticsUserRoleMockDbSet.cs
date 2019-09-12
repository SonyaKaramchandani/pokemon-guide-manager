using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    class ZebraAnalyticsUserRoleMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly string USER_ROLE_1_ID = Guid.NewGuid().ToString();
        public static readonly string USER_ROLE_1_NAME = random.Next(1000000, 9999999).ToString();

        public Mock<RoleManager<IdentityRole>> MockContext { get; set; }

        public ZebraAnalyticsUserRoleMockDbSet()
        {
            MockContext = new Mock<RoleManager<IdentityRole>>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            MockContext.Setup(context => context.Roles).Returns(() => CreateMockedUserRoles());
        }

        private IQueryable<IdentityRole> CreateMockedUserRoles()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = USER_ROLE_1_ID,
                    Name = USER_ROLE_1_NAME
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = random.Next(1, 100000).ToString()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = random.Next(1, 100000).ToString()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = random.Next(1, 100000).ToString()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = random.Next(1, 100000).ToString()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = random.Next(1, 100000).ToString()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = random.Next(1, 100000).ToString()
                }
            }.AsQueryable();
        }
    }
}
