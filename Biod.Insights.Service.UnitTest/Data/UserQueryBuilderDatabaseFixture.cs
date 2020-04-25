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

        public static readonly AspNetUsers USER_A = new AspNetUsers
        {
            Id = "ds90fua5jds9f0a9sdf",
            AspNetUserRoles = null
        };

        public static readonly AspNetUsers USER_B = new AspNetUsers
        {
            Id = "98hgr98ega6sdf32gda",
            AspNetUserRoles = new List<AspNetUserRoles>()
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

        public BiodZebraContext DbContext { get; }

        public UserQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("UserQueryBuilderDatabaseFixture")
                .Options);

            DbContext.AspNetUsers.Add(USER_A);
            DbContext.AspNetUsers.Add(USER_B);
            DbContext.AspNetUsers.Add(USER_C);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}