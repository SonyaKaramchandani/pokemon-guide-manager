using System;
using System.Linq;
using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class GeonameConfigTest
    {
        [Fact]
        public void GeonameConfigBuilder_Default()
        {
            var config = new GeonameConfig.Builder().Build();

            Assert.Empty(config.GeonameIds);
            Assert.False(config.IncludeShape);
        }

        [Fact]
        public void GeonameConfigBuilder_GeonameId()
        {
            var geonameId = new Random().Next(0, int.MaxValue);
            var config = new GeonameConfig.Builder()
                .AddGeonameId(geonameId)
                .Build();
            Assert.Contains(geonameId, config.GeonameIds);
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new[] {1})]
        [InlineData(new[] {414, 213, 513, 32, 62, 1508})]
        public void GeonameConfigBuilder_GeonameIds(int[] geonameIds)
        {
            var config = new GeonameConfig.Builder()
                .AddGeonameIds(geonameIds)
                .Build();
            Assert.Equal(geonameIds.OrderBy(d => d), config.GeonameIds.OrderBy(d => d));
        }

        [Fact]
        public void GeonameConfigBuilder_SingleGeonameId()
        {
            var config = new GeonameConfig.Builder()
                .AddGeonameIds(new[] {123})
                .Build();

            Assert.Equal(123, config.GetGeonameId());
        }

        [Fact]
        public void GeonameConfigBuilder_SingleGeonameIdException()
        {
            var config = new GeonameConfig.Builder()
                .AddGeonameIds(new[] {1, 2, 3})
                .Build();

            Assert.Throws<InvalidOperationException>(() => config.GetGeonameId());
        }

        [Fact]
        public void GeonameConfigBuilder_IncludeShape()
        {
            var config = new GeonameConfig.Builder()
                .ShouldIncludeShape()
                .Build();

            Assert.True(config.IncludeShape);
        }
    }
}