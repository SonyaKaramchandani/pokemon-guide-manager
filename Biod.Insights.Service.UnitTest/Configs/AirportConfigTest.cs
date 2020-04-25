using System;
using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class AirportConfigTest
    {
        [Fact]
        public void AirportConfigBuilder_Default()
        {
            var config = new AirportConfig.Builder(1).Build();

            Assert.False(config.IncludeCity);
            Assert.False(config.IncludeImportationRisk);
            Assert.False(config.IncludeExportationRisk);
            Assert.Null(config.GeonameId);
        }

        [Fact]
        public void AirportConfigBuilder_EventId()
        {
            var eventId = new Random().Next(0, int.MaxValue);
            var config = new AirportConfig.Builder(eventId).Build();
            Assert.Equal(eventId, config.EventId);
        }

        [Fact]
        public void AirportConfigBuilder_IncludeCity()
        {
            var config = new AirportConfig.Builder(1)
                .ShouldIncludeCity()
                .Build();

            Assert.True(config.IncludeCity);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(23124)]
        public void AirportConfigBuilder_IncludeImportationRisk(int? geonameId)
        {
            var config = new AirportConfig.Builder(1)
                .ShouldIncludeImportationRisk(geonameId)
                .Build();

            Assert.True(config.IncludeImportationRisk);
            Assert.Equal(geonameId, config.GeonameId);
        }

        [Fact]
        public void AirportConfigBuilder_IncludeExportationRisk()
        {
            var config = new AirportConfig.Builder(1)
                .ShouldIncludeExportationRisk()
                .Build();

            Assert.True(config.IncludeExportationRisk);
        }
    }
}