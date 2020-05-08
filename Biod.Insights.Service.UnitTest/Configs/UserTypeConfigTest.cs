using System;
using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class UserTypeConfigTest
    {
        [Fact]
        public void UserTypeConfig_Default()
        {
            var config = new UserTypeConfig.Builder().Build();

            Assert.Null(config.UserTypeId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("1b6b1afb-21c7-4621-94f3-78c273c3cb8a")]
        public void UserTypeConfig_UserTypeId(string userTypeId)
        {
            var config = new UserTypeConfig.Builder()
                .SetUserTypeId(userTypeId == null ? null : (Guid?) Guid.Parse(userTypeId))
                .Build();

            Assert.Equal(userTypeId, config.UserTypeId?.ToString());
        }
    }
}