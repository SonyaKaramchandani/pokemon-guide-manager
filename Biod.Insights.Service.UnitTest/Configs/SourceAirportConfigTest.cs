using System;
using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class SourceAirportConfigTest
    {
        [Fact]
        public void SourceAirportConfigBuilder_Default()
        {
            var config = new SourceAirportConfig.Builder(1).Build();

            Assert.False(config.IncludeCity);
            Assert.False(config.IncludeImportationRisk);
            Assert.False(config.IncludeExportationRisk);
            Assert.False(config.IncludePopulation);
            Assert.False(config.IncludeCaseCounts);
            Assert.Null(config.GeonameId);
        }

        [Fact]
        public void SourceAirportConfigBuilder_EventId()
        {
            var eventId = new Random().Next(0, int.MaxValue);
            var config = new SourceAirportConfig.Builder(eventId).Build();
            Assert.Equal(eventId, config.EventId);
        }

        [Fact]
        public void SourceAirportConfigBuilder_IncludeCity()
        {
            var config = new SourceAirportConfig.Builder(1)
                .ShouldIncludeCity()
                .Build();

            Assert.True(config.IncludeCity);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(23124)]
        public void SourceAirportConfigBuilder_IncludeImportationRisk(int? geonameId)
        {
            var config = new SourceAirportConfig.Builder(1)
                .ShouldIncludeImportationRisk(geonameId)
                .Build();

            Assert.True(config.IncludeImportationRisk);
            Assert.Equal(geonameId, config.GeonameId);
        }

        [Fact]
        public void SourceAirportConfigBuilder_IncludeExportationRisk()
        {
            var config = new SourceAirportConfig.Builder(1)
                .ShouldIncludeExportationRisk()
                .Build();

            Assert.True(config.IncludeExportationRisk);
        }

        [Fact]
        public void SourceAirportConfigBuilder_IncludePopulation()
        {
            var config = new SourceAirportConfig.Builder(1)
                .ShouldIncludePopulation()
                .Build();

            Assert.True(config.IncludePopulation);
        }

        [Fact]
        public void SourceAirportConfigBuilder_IncludeCaseCounts()
        {
            var config = new SourceAirportConfig.Builder(1)
                .ShouldIncludeCaseCounts()
                .Build();

            Assert.True(config.IncludeCaseCounts);
        }
    }
}