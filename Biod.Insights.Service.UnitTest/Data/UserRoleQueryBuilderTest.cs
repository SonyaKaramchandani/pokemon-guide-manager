using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.QueryBuilders;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class UserRoleQueryBuilderTest : IClassFixture<UserRoleQueryBuilderDatabaseFixture>
    {
        private readonly UserRoleQueryBuilderDatabaseFixture _fixture;

        public UserRoleQueryBuilderTest(UserRoleQueryBuilderDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void UserRoleQueryBuilder_OverrideInitialQueryable()
        {
            var queryable = new List<AspNetRoles>().AsQueryable();
            var queryBuilder = new UserRoleQueryBuilder(_fixture.DbContext).OverrideInitialQueryable(queryable);
            Assert.Equal(queryable, queryBuilder.GetInitialQueryable());
        }

        [Fact]
        public async Task UserRoleQueryBuilder_NoConfig()
        {
            var result = await new UserRoleQueryBuilder(_fixture.DbContext).BuildAndExecute();
            Assert.Equal(new[]
                {
                    "9dho983ng9sh",
                    "b98h54lds579",
                    "h89snl3k5hkd",
                    "s9ad7fh98ydf"
                },
                result.Select(r => r.Id).OrderBy(r => r));
        }

        [Fact]
        public async Task UserRoleQueryBuilder_SingleRole()
        {
            var result = (await new UserRoleQueryBuilder(_fixture.DbContext, new UserRoleConfig.Builder()
                    .SetRoleId("h89snl3k5hkd")
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Single(result);
            Assert.Equal("h89snl3k5hkd", result.Single().Id);
        }

        [Fact]
        public async Task UserRoleQueryBuilder_NonExistentRole()
        {
            var result = (await new UserRoleQueryBuilder(_fixture.DbContext, new UserRoleConfig.Builder()
                    .SetRoleId("d89uyf4dhs89af8sf")
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Empty(result);
        }

        [Fact]
        public async Task UserRoleQueryBuilder_PublicRoles()
        {
            var result = (await new UserRoleQueryBuilder(_fixture.DbContext, new UserRoleConfig.Builder()
                    .ShouldIncludePublicRolesOnly()
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(new[] {"9dho983ng9sh", "h89snl3k5hkd"}, result.Select(r => r.Id).OrderBy(r => r));
        }

        [Fact]
        public async Task UserRoleQueryBuilder_PrivateRoles()
        {
            var result = (await new UserRoleQueryBuilder(_fixture.DbContext, new UserRoleConfig.Builder()
                    .ShouldIncludePrivateRolesOnly()
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(new[] {"b98h54lds579", "s9ad7fh98ydf"}, result.Select(r => r.Id).OrderBy(r => r));
        }
    }
}