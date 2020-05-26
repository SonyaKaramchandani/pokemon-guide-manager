using System;
using System.Linq;
using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class EventConfigTest
    {
        [Fact]
        public void EventConfig_Default()
        {
            var config = new EventConfig.Builder().Build();

            Assert.Null(config.EventId);
            Assert.Null(config.GeonameId);
            Assert.Empty(config.DiseaseIds);
            Assert.False(config.IncludeArticles);
            Assert.False(config.IncludeProximalCaseCountDistribution);
            Assert.False(config.IncludeLocations);
            Assert.False(config.IncludeLocationsHistory);
            Assert.False(config.IncludeExportationRisk);
            Assert.False(config.IncludeImportationRisk);
            Assert.Null(config.SourceAirportConfig);
            Assert.False(config.IncludeSourceAirports);
            Assert.Null(config.DestinationAirportConfig);
            Assert.False(config.IncludeDestinationAirports);
            Assert.False(config.IncludeDiseaseInformation);
            Assert.False(config.IncludeOutbreakPotential);
            Assert.False(config.IncludeCalculationMetadata);
        }

        [Fact]
        public void EventConfigBuilder_EventId()
        {
            var eventId = new Random().Next(0, int.MaxValue);
            var config = new EventConfig.Builder()
                .SetEventId(eventId)
                .Build();
            Assert.Equal(eventId, config.EventId);
        }

        [Fact]
        public void EventConfigBuilder_DiseaseId()
        {
            var diseaseId = new Random().Next(0, int.MaxValue);
            var config = new EventConfig.Builder()
                .AddDiseaseId(diseaseId)
                .Build();
            Assert.Contains(diseaseId, config.DiseaseIds);
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new[] {1})]
        [InlineData(new[] {414, 213, 513, 32, 62, 1508})]
        public void EventConfigBuilder_DiseaseIds(int[] diseaseIds)
        {
            var config = new EventConfig.Builder()
                .AddDiseaseIds(diseaseIds)
                .Build();
            Assert.Equal(diseaseIds.OrderBy(d => d), config.DiseaseIds.OrderBy(d => d));
        }

        [Fact]
        public void EventConfigBuilder_IncludeArticles()
        {
            var config = new EventConfig.Builder()
                .ShouldIncludeArticles()
                .Build();

            Assert.True(config.IncludeArticles);
        }

        [Fact]
        public void EventConfigBuilder_IncludeLocalCaseCount()
        {
            var geonameId = new Random().Next(0, int.MaxValue);
            var config = new EventConfig.Builder()
                .ShouldIncludeProximalCaseCountDistribution(geonameId)
                .Build();

            Assert.True(config.IncludeProximalCaseCountDistribution);
            Assert.Equal(geonameId, config.GeonameId);
        }

        [Fact]
        public void EventConfigBuilder_IncludeLocations()
        {
            var config = new EventConfig.Builder()
                .ShouldIncludeLocations()
                .Build();

            Assert.True(config.IncludeLocations);
        }

        [Fact]
        public void EventConfigBuilder_IncludeLocationsHistory()
        {
            var config = new EventConfig.Builder()
                .ShouldIncludeLocationsHistory()
                .Build();

            Assert.True(config.IncludeLocationsHistory);
        }

        [Fact]
        public void EventConfigBuilder_IncludeExportationRisk()
        {
            var config = new EventConfig.Builder()
                .ShouldIncludeExportationRisk()
                .Build();

            Assert.True(config.IncludeExportationRisk);
        }

        [Fact]
        public void EventConfigBuilder_IncludeImportationRisk()
        {
            var geonameId = new Random().Next(0, int.MaxValue);
            var config = new EventConfig.Builder()
                .ShouldIncludeImportationRisk(geonameId)
                .Build();

            Assert.True(config.IncludeImportationRisk);
            Assert.Equal(geonameId, config.GeonameId);
        }

        [Fact]
        public void EventConfigBuilder_IncludeSourceAirports()
        {
            var airportConfig = new SourceAirportConfig.Builder(12).Build();
            var config = new EventConfig.Builder()
                .ShouldIncludeSourceAirports(airportConfig)
                .Build();

            Assert.True(config.IncludeSourceAirports);
            Assert.Equal(airportConfig, config.SourceAirportConfig);
        }

        [Fact]
        public void EventConfigBuilder_IncludeDestinationAirports()
        {
            var airportConfig = new AirportConfig.Builder(12).Build();
            var config = new EventConfig.Builder()
                .ShouldIncludeDestinationAirports(airportConfig)
                .Build();

            Assert.True(config.IncludeDestinationAirports);
            Assert.True(config.IncludeLocations);
            Assert.Equal(airportConfig, config.DestinationAirportConfig);
        }

        [Fact]
        public void EventConfigBuilder_IncludeDiseaseInformation()
        {
            var config = new EventConfig.Builder()
                .ShouldIncludeDiseaseInformation()
                .Build();

            Assert.True(config.IncludeDiseaseInformation);
        }

        [Fact]
        public void EventConfigBuilder_IncludeCalculationMetadata()
        {
            var config = new EventConfig.Builder()
                .ShouldIncludeCalculationMetadata()
                .Build();

            Assert.True(config.IncludeCalculationMetadata);
        }

        [Fact]
        public void EventConfigBuilder_SingleGeonameId()
        {
            var geonameId = new Random().Next(0, int.MaxValue);
            var config = new EventConfig.Builder()
                .ShouldIncludeProximalCaseCountDistribution(geonameId)
                .ShouldIncludeImportationRisk(geonameId)
                .Build();

            Assert.Equal(geonameId, config.GeonameId);
        }

        [Fact]
        public void EventConfigBuilder_SingleGeonameIdException()
        {
            var configBuilder = new EventConfig.Builder()
                .ShouldIncludeProximalCaseCountDistribution(123);

            Assert.Throws<InvalidOperationException>(() => configBuilder.ShouldIncludeImportationRisk(456));
        }
    }
}