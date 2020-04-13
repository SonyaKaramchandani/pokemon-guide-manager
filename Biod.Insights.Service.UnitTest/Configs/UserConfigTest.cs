using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class UserConfigTest
    {
        [Fact]
        public void UserConfig_Default()
        {
            var config = new UserConfig.Builder().Build();

            Assert.Null(config.UserId);
            Assert.False(config.TrackChanges);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("f87219h9f87dafhl")]
        public void DiseaseConfigBuilder_UserId(string userId)
        {
            var config = new UserConfig.Builder()
                .SetUserId(userId)
                .Build();

            Assert.Equal(userId, config.UserId);
        }

        [Fact]
        public void DiseaseConfigBuilder_TrackChanges()
        {
            var config = new UserConfig.Builder()
                .ShouldTrackChanges()
                .Build();

            Assert.True(config.TrackChanges);
        }
    }
}