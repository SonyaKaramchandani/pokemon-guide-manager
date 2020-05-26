using System;
using System.Collections.Generic;
using Biod.Insights.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class UserQueryBuilderDatabaseFixture : IDisposable
    {
        public static readonly AspNetRoles ROLE_A = new AspNetRoles {Id = "df90j"};
        public static readonly AspNetRoles ROLE_B = new AspNetRoles {Id = "g2gre"};

        public static readonly UserTypes USER_TYPE_A = new UserTypes {Id = Guid.Parse("a3f3300e-5495-4127-ad6a-0d35850e032c")};
        public static readonly UserTypes USER_TYPE_B = new UserTypes {Id = Guid.Parse("c0a7e7ec-4365-45e3-a9a6-6c8b8c9d9298")};
        public static readonly UserTypes USER_TYPE_C = new UserTypes {Id = Guid.Parse("e5f9037e-fe45-4e3f-acb3-649e1f1ffcff")};

        public static readonly AspNetUsers USER_A = new AspNetUsers
        {
            Id = "ds90fua5jds9f0a9sdf",
            AspNetUserRoles = null
        };

        public static readonly UserProfile USER_PROFILE_A = new UserProfile
        {
            Id = "ds90fua5jds9f0a9sdf",
            UserType = USER_TYPE_A
        };

        public static readonly AspNetUsers USER_B = new AspNetUsers
        {
            Id = "98hgr98ega6sdf32gda",
            AspNetUserRoles = new List<AspNetUserRoles>()
        };

        public static readonly UserProfile USER_PROFILE_B = new UserProfile
        {
            Id = "98hgr98ega6sdf32gda",
            UserType = USER_TYPE_B
        };

        public static readonly AspNetUsers USER_C = new AspNetUsers
        {
            Id = "zbh9js0f98hnm4su4gl",
            AspNetUserRoles = new List<AspNetUserRoles>
            {
                new AspNetUserRoles {Role = ROLE_A},
                new AspNetUserRoles {Role = ROLE_B}
            }
        };

        public static readonly UserProfile USER_PROFILE_C = new UserProfile
        {
            Id = "zbh9js0f98hnm4su4gl",
            UserType = USER_TYPE_C
        };

        public BiodZebraContext DbContext { get; }

        public UserQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("UserQueryBuilderDatabaseFixture")
                .Options);

            DbContext.AspNetUsers.Add(USER_A);
            DbContext.AspNetUsers.Add(USER_B);
            DbContext.AspNetUsers.Add(USER_C);

            DbContext.UserProfile.Add(USER_PROFILE_A);
            DbContext.UserProfile.Add(USER_PROFILE_B);
            DbContext.UserProfile.Add(USER_PROFILE_C);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}