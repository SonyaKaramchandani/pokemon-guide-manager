using System;
using Biod.Insights.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class UserTypeQueryBuilderDatabaseFixture : IDisposable
    {
        public static readonly UserTypes USER_TYPE_A = new UserTypes {Id = Guid.Parse("fe24cf02-f472-4b4e-af72-f0abccd26831")};
        public static readonly UserTypes USER_TYPE_B = new UserTypes {Id = Guid.Parse("717a9d6f-4f34-46b8-aef9-1ddaf7c2207d")};
        public static readonly UserTypes USER_TYPE_C = new UserTypes {Id = Guid.Parse("0e23cf99-bead-4f2f-8eba-6e76907df223")};
        public static readonly UserTypes USER_TYPE_D = new UserTypes {Id = Guid.Parse("fd223406-7c00-4cb7-9969-c41c281e8636")};

        public BiodZebraContext DbContext { get; }

        public UserTypeQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("UserTypeQueryBuilderDatabaseFixture")
                .Options);

            DbContext.UserTypes.Add(USER_TYPE_A);
            DbContext.UserTypes.Add(USER_TYPE_B);
            DbContext.UserTypes.Add(USER_TYPE_C);
            DbContext.UserTypes.Add(USER_TYPE_D);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}