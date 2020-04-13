using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class UserRoleConfigTest
    {
        [Fact]
        public void UserRoleConfig_Default()
        {
            var config = new UserRoleConfig.Builder().Build();

            Assert.Null(config.RoleId);
            Assert.False(config.IncludePublicRolesOnly);
            Assert.False(config.IncludePrivateRolesOnly);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("s8dfh9a8yd2fg9as8df")]
        public void UserRoleConfig_RoleId(string roleId)
        {
            var config = new UserRoleConfig.Builder()
                .SetRoleId(roleId)
                .Build();

            Assert.Equal(roleId, config.RoleId);
        }

        [Fact]
        public void UserRoleConfigBuilder_IncludePublicRolesOnly()
        {
            var config = new UserRoleConfig.Builder()
                .ShouldIncludePrivateRolesOnly()
                .Build();

            Assert.True(config.IncludePrivateRolesOnly);
            Assert.False(config.IncludePublicRolesOnly);
        }

        [Fact]
        public void UserRoleConfigBuilder_IncludePrivateRolesOnly()
        {
            var config = new UserRoleConfig.Builder()
                .ShouldIncludePublicRolesOnly()
                .Build();

            Assert.True(config.IncludePublicRolesOnly);
            Assert.False(config.IncludePrivateRolesOnly);
        }
    }
}