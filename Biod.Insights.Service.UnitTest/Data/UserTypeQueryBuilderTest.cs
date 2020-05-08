using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.QueryBuilders;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class UserTypeQueryBuilderTest : IClassFixture<UserTypeQueryBuilderDatabaseFixture>
    {
        private readonly UserTypeQueryBuilderDatabaseFixture _fixture;

        public UserTypeQueryBuilderTest(UserTypeQueryBuilderDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void UserTypeQueryBuilder_OverrideInitialQueryable()
        {
            var queryable = new List<UserTypes>().AsQueryable();
            var queryBuilder = new UserTypeQueryBuilder(_fixture.DbContext).OverrideInitialQueryable(queryable);
            Assert.Equal(queryable, queryBuilder.GetInitialQueryable());
        }

        [Fact]
        public async Task UserTypeQueryBuilder_NoConfig()
        {
            var result = await new UserTypeQueryBuilder(_fixture.DbContext).BuildAndExecute();
            Assert.Equal(new[]
                {
                    Guid.Parse("0e23cf99-bead-4f2f-8eba-6e76907df223"),
                    Guid.Parse("717a9d6f-4f34-46b8-aef9-1ddaf7c2207d"),
                    Guid.Parse("fd223406-7c00-4cb7-9969-c41c281e8636"),
                    Guid.Parse("fe24cf02-f472-4b4e-af72-f0abccd26831")
                },
                result.Select(r => r.Id).OrderBy(r => r));
        }

        [Fact]
        public async Task UserTypeQueryBuilder_SingleType()
        {
            var result = (await new UserTypeQueryBuilder(_fixture.DbContext, new UserTypeConfig.Builder()
                    .SetUserTypeId(Guid.Parse("0e23cf99-bead-4f2f-8eba-6e76907df223"))
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Single(result);
            Assert.Equal(Guid.Parse("0e23cf99-bead-4f2f-8eba-6e76907df223"), result.Single().Id);
        }

        [Fact]
        public async Task UserTypeQueryBuilder_NonExistentType()
        {
            var result = (await new UserTypeQueryBuilder(_fixture.DbContext, new UserTypeConfig.Builder()
                    .SetUserTypeId(Guid.Parse("7032a10c-c61c-47a6-85f7-1d53e9badd2e"))
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Empty(result);
        }
    }
}