using System;
using Biod.Insights.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class UserRoleQueryBuilderDatabaseFixture : IDisposable
    {
        public static readonly AspNetRoles ROLE_A = new AspNetRoles {Id = "s9ad7fh98ydf", IsPublic = false};
        public static readonly AspNetRoles ROLE_B = new AspNetRoles {Id = "h89snl3k5hkd", IsPublic = true};
        public static readonly AspNetRoles ROLE_C = new AspNetRoles {Id = "9dho983ng9sh", IsPublic = true};
        public static readonly AspNetRoles ROLE_D = new AspNetRoles {Id = "b98h54lds579", IsPublic = false};

        public BiodZebraContext DbContext { get; }

        public UserRoleQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("UserRoleQueryBuilderDatabaseFixture")
                .Options);

            DbContext.AspNetRoles.Add(ROLE_A);
            DbContext.AspNetRoles.Add(ROLE_B);
            DbContext.AspNetRoles.Add(ROLE_C);
            DbContext.AspNetRoles.Add(ROLE_D);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}