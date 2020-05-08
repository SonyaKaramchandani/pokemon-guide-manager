using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.QueryBuilders;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class UserQueryBuilderTest : IClassFixture<UserQueryBuilderDatabaseFixture>
    {
        private readonly UserQueryBuilderDatabaseFixture _fixture;

        public UserQueryBuilderTest(UserQueryBuilderDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void UserQueryBuilder_OverrideInitialQueryable()
        {
            var queryable = new List<AspNetUsers>().AsQueryable();
            var queryBuilder = new UserQueryBuilder(_fixture.DbContext).OverrideInitialQueryable(queryable);
            Assert.Equal(queryable, queryBuilder.GetInitialQueryable());
        }

        [Fact]
        public async Task UserQueryBuilder_NoConfig()
        {
            var result = await new UserQueryBuilder(_fixture.DbContext).BuildAndExecute();
            Assert.Equal(new[]
                {
                    "98hgr98ega6sdf32gda",
                    "ds90fua5jds9f0a9sdf",
                    "zbh9js0f98hnm4su4gl"
                },
                result.Select(u => u.User.Id).OrderBy(u => u));
        }

        [Fact]
        public async Task UserQueryBuilder_SingleUser()
        {
            var result = (await new UserQueryBuilder(_fixture.DbContext, new UserConfig.Builder()
                    .SetUserId("98hgr98ega6sdf32gda")
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Single(result);
            Assert.Equal("98hgr98ega6sdf32gda", result.Single().User.Id);
        }

        [Fact]
        public async Task UserQueryBuilder_NonExistentUser()
        {
            var result = (await new UserQueryBuilder(_fixture.DbContext, new UserConfig.Builder()
                    .SetUserId("d89uyf4dhs89af8sf")
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("ds90fua5jds9f0a9sdf", new string[0])]
        [InlineData("98hgr98ega6sdf32gda", new string[0])]
        [InlineData("zbh9js0f98hnm4su4gl", new[] {"df90j", "g2gre"})]
        public async Task UserQueryBuilder_Roles(string userId, string[] expectedRoleIds)
        {
            var result = (await new UserQueryBuilder(_fixture.DbContext, new UserConfig.Builder()
                    .SetUserId(userId)
                    .Build()).BuildAndExecute())
                .ToList();

            Assert.Equal(expectedRoleIds, result.Single().Roles.Select(r => r.Id).OrderBy(r => r).ToArray());
        }

        [Theory]
        [InlineData("ds90fua5jds9f0a9sdf", "a3f3300e-5495-4127-ad6a-0d35850e032c")]
        [InlineData("98hgr98ega6sdf32gda", "c0a7e7ec-4365-45e3-a9a6-6c8b8c9d9298")]
        [InlineData("zbh9js0f98hnm4su4gl", "e5f9037e-fe45-4e3f-acb3-649e1f1ffcff")]
        public async Task UserQueryBuilder_UserType(string userId, string expectedUserTypeId)
        {
            var result = (await new UserQueryBuilder(_fixture.DbContext, new UserConfig.Builder()
                    .SetUserId(userId)
                    .Build()).BuildAndExecute())
                .ToList();

            Assert.Equal(expectedUserTypeId, result.Single().UserType.Id.ToString());
        }
    }
}