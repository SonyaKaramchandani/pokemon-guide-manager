using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Products.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.QueryBuilders;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class DestinationAirportQueryBuilderTest : IClassFixture<DestinationAirportQueryBuilderDatabaseFixture>, IDisposable
    {
        private readonly DestinationAirportQueryBuilderDatabaseFixture _fixture;

        public DestinationAirportQueryBuilderTest(DestinationAirportQueryBuilderDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            // Clean up the Configuration Variables between each test
            _fixture.DbContext.ConfigurationVariables.RemoveRange(_fixture.DbContext.ConfigurationVariables);
            _fixture.DbContext.SaveChanges();
        }

        [Fact]
        public void DestinationAirportQueryBuilder_OverrideInitialQueryable()
        {
            var queryable = new List<EventDestinationAirportSpreadMd>().AsQueryable();
            var queryBuilder = new DestinationAirportQueryBuilder(
                    _fixture.DbContext,
                    new AirportConfig.Builder(1).Build())
                .OverrideInitialQueryable(queryable);
            Assert.Equal(queryable, queryBuilder.GetInitialQueryable());
        }

        [Theory]
        [InlineData(999, new int[0])]
        [InlineData(1, new[] {9968})]
        [InlineData(2, new[] {7740, 9968, 10711})]
        [InlineData(11, new[] {10357})]
        [InlineData(12, new[] {10711})]
        public async Task DestinationAirportQueryBuilder_StationIds(int eventId, int[] expectedDestinationAirportIds)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .ToArray();

            Assert.Equal(expectedDestinationAirportIds.Length, result.Length);
            Assert.Equal(expectedDestinationAirportIds, result.Select(a => a.StationId).OrderBy(d => d));
        }

        [Theory]
        // Cities
        [InlineData(999, 1, new int[0])]
        [InlineData(999, 2, new int[0])]
        [InlineData(999, 3, new int[0])]
        [InlineData(999, 4, new int[0])]
        [InlineData(1, 1, new int[0])] // TPE not in AA-##
        [InlineData(1, 2, new[] {9968})] // TPE in AB-##
        [InlineData(1, 3, new[] {9968})] // TPE in {AA-##, AB-##}
        [InlineData(1, 4, new[] {9968})] // TPE in {AA-##, AB-##, AC-##}
        [InlineData(2, 1, new int[0])] // CDG or TPE or YVR not in AA-##
        [InlineData(2, 2, new[] {9968})] // TPE in AB-##
        [InlineData(2, 3, new[] {9968})] // TPE in {AA-##, AB-##}
        [InlineData(2, 4, new[] {9968, 10711})] // TPE and YVR in {AA-##, AB-##, AC-##}
        [InlineData(11, 1, new[] {10357})] // YYZ in AA-##
        [InlineData(11, 2, new int[0])] // YYZ not in AB-##
        [InlineData(11, 3, new[] {10357})] // YYZ in {AA-##, AB-##}
        [InlineData(11, 4, new[] {10357})] // YYZ in {AA-##, AB-##, AC-##}
        [InlineData(12, 1, new int[0])] // YVR not in AA-##
        [InlineData(12, 2, new int[0])] // YVR not in AB-##
        [InlineData(12, 3, new int[0])] // YVR not in {AA-##, AB-##}
        [InlineData(12, 4, new[] {10711})] // YVR in {AA-##, AB-##, AC-##}
        // Provinces
        [InlineData(999, 5, new int[0])]
        [InlineData(999, 6, new int[0])]
        [InlineData(999, 7, new int[0])]
        [InlineData(999, 8, new int[0])]
        [InlineData(1, 5, new int[0])] // TPE not in AC-##
        [InlineData(1, 6, new int[0])] // TPE not in AA-##
        [InlineData(1, 7, new[] {9968})] // TPE in {AB-##, AC-##}
        [InlineData(1, 8, new[] {9968})] // TPE in {AA-##, AB-##, AC-##}
        [InlineData(2, 5, new[] {10711})] // YVR in AC-##
        [InlineData(2, 6, new int[0])] // CDG or TPE or YVR not in AA-##
        [InlineData(2, 7, new[] {9968, 10711})] // TPE and YVR in {AB-##, AC-##}
        [InlineData(2, 8, new[] {9968, 10711})] // TPE and YVR in {AA-##, AB-##, AC-##}
        [InlineData(11, 5, new int[0])] // YYZ not in AC-##
        [InlineData(11, 6, new[] {10357})] // YYZ in AA-##
        [InlineData(11, 7, new int[0])] // YYZ not in {AB-##, AC-##}
        [InlineData(11, 8, new[] {10357})] // YYZ in {AA-##, AB-##, AC-##}
        [InlineData(12, 5, new[] {10711})] // YVR in AC-##
        [InlineData(12, 6, new int[0])] // YVR not in AA-##
        [InlineData(12, 7, new[] {10711})] // YVR in {AB-##, AC-##}
        [InlineData(12, 8, new[] {10711})] // YVR in {AA-##, AB-##, AC-##}
        // Countries
        [InlineData(999, 9, new int[0])]
        [InlineData(999, 10, new int[0])]
        [InlineData(999, 11, new int[0])]
        [InlineData(999, 12, new int[0])]
        [InlineData(1, 9, new[] {9968})] // TPE in AB-##
        [InlineData(1, 10, new int[0])] // TPE not in AC-##
        [InlineData(1, 11, new int[0])] // TPE not in {AA-##, AC-##}
        [InlineData(1, 12, new[] {9968})] // TPE in {AA-##, AB-##, AC-##}
        [InlineData(2, 9, new[] {9968})] // TPE in AB-##
        [InlineData(2, 10, new[] {10711})] // YVR in AC-##
        [InlineData(2, 11, new[] {10711})] // YVR in {AA-##, AC-##}
        [InlineData(2, 12, new[] {9968, 10711})] // TPE and YVR in {AA-##, AB-##, AC-##}
        [InlineData(11, 9, new int[0])] // YYZ not in AB-##
        [InlineData(11, 10, new int[0])] // YYZ not in AC-##
        [InlineData(11, 11, new[] {10357})] // YYZ in {AA-##, AC-##}
        [InlineData(11, 12, new[] {10357})] // YYZ in {AA-##, AB-##, AC-##}
        [InlineData(12, 9, new int[0])] // YVR not in AB-##
        [InlineData(12, 10, new[] {10711})] // YVR in AC-##
        [InlineData(12, 11, new[] {10711})] // YVR in {AA-##, AC-##}
        [InlineData(12, 12, new[] {10711})] // YVR in {AA-##, AB-##, AC-##}
        public async Task DestinationAirportQueryBuilder_StationIdsInLocationRange(int eventId, int geonameId, int[] expectedDestinationAirportIds)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).ShouldIncludeImportationRisk(geonameId).Build())
                    .BuildAndExecute())
                .ToArray();

            Assert.Equal(expectedDestinationAirportIds.Length, result.Length);
            Assert.Equal(expectedDestinationAirportIds, result.Select(a => a.StationId).OrderBy(d => d));
        }

        [Theory]
        // No Threshold
        [InlineData(999, 1, "0.0", new int[0])]
        [InlineData(999, 2, "0.0", new int[0])]
        [InlineData(999, 3, "0.0", new int[0])]
        [InlineData(999, 4, "0.0", new int[0])]
        [InlineData(1, 1, "0.0", new int[0])] // TPE not in AA-##
        [InlineData(1, 2, "0.0", new[] {9968})] // 0.2 and 0.8
        [InlineData(1, 3, "0.0", new[] {9968})] // 0.2 and 0.8
        [InlineData(1, 4, "0.0", new[] {9968})] // 0.2 and 0.8
        [InlineData(2, 1, "0.0", new int[0])] // CDG or TPE or YVR not in AA-##
        [InlineData(2, 2, "0.0", new[] {9968})] // 0.2 and 0.8
        [InlineData(2, 3, "0.0", new[] {9968})] // 0.2 and 0.8
        [InlineData(2, 4, "0.0", new[] {9968, 10711})] // 0.2 and 0.8; 0.011 and 0.989
        [InlineData(11, 1, "0.0", new[] {10357})] // 0.5
        [InlineData(11, 2, "0.0", new int[0])] // YYZ not in AB-##
        [InlineData(11, 3, "0.0", new[] {10357})] // 0.5
        [InlineData(11, 4, "0.0", new[] {10357})] // 0.5
        [InlineData(12, 1, "0.0", new int[0])] // YVR not in AA-##
        [InlineData(12, 2, "0.0", new int[0])] // YVR not in AB-##
        [InlineData(12, 3, "0.0", new int[0])] // YVR not in {AA-##, AB-##}
        [InlineData(12, 4, "0.0", new[] {10711})] // 0.011 and 0.989
        // Medium Threshold
        [InlineData(999, 1, "0.51", new int[0])]
        [InlineData(999, 2, "0.51", new int[0])]
        [InlineData(999, 3, "0.51", new int[0])]
        [InlineData(999, 4, "0.51", new int[0])]
        [InlineData(1, 1, "0.51", new int[0])] // TPE not in AA-##
        [InlineData(1, 2, "0.51", new[] {9968})] // 0.8
        [InlineData(1, 3, "0.51", new[] {9968})] // 0.8
        [InlineData(1, 4, "0.51", new[] {9968})] // 0.8
        [InlineData(2, 1, "0.51", new int[0])] // CDG or TPE or YVR not in AA-##
        [InlineData(2, 2, "0.51", new[] {9968})] // 0.8
        [InlineData(2, 3, "0.51", new[] {9968})] // 0.8
        [InlineData(2, 4, "0.51", new[] {9968, 10711})] //0.8; 0.989
        [InlineData(11, 1, "0.51", new int[0])] // Not in threshold (0.5)
        [InlineData(11, 2, "0.51", new int[0])] // YYZ not in AB-##
        [InlineData(11, 3, "0.51", new int[0])] // Not in threshold (0.5)
        [InlineData(11, 4, "0.51", new int[0])] // Not in threshold (0.5)
        [InlineData(12, 1, "0.51", new int[0])] // YVR not in AA-##
        [InlineData(12, 2, "0.51", new int[0])] // YVR not in AB-##
        [InlineData(12, 3, "0.51", new int[0])] // YVR not in {AA-##, AB-##}
        [InlineData(12, 4, "0.51", new[] {10711})] // 0.989
        // High Threshold
        [InlineData(999, 1, "0.9", new int[0])]
        [InlineData(999, 2, "0.9", new int[0])]
        [InlineData(999, 3, "0.9", new int[0])]
        [InlineData(999, 4, "0.9", new int[0])]
        [InlineData(1, 1, "0.9", new int[0])] // TPE not in AA-##
        [InlineData(1, 2, "0.9", new int[0])] // Not in threshold (0.2 and 0.8)
        [InlineData(1, 3, "0.9", new int[0])] // Not in threshold (0.2 and 0.8)
        [InlineData(1, 4, "0.9", new int[0])] // Not in threshold (0.2 and 0.8)
        [InlineData(2, 1, "0.9", new int[0])] // CDG or TPE or YVR not in AA-##
        [InlineData(2, 2, "0.9", new int[0])] // Not in threshold (0.2 and 0.8)
        [InlineData(2, 3, "0.9", new int[0])] // Not in threshold (0.2 and 0.8)
        [InlineData(2, 4, "0.9", new[] {10711})] // Not in threshold (0.2 and 0.8); 0.989
        [InlineData(11, 1, "0.9", new int[0])] // Not in threshold (0.5)
        [InlineData(11, 2, "0.9", new int[0])] // YYZ not in AB-##
        [InlineData(11, 3, "0.9", new int[0])] // Not in threshold (0.5)
        [InlineData(11, 4, "0.9", new int[0])] // Not in threshold (0.5)
        [InlineData(12, 1, "0.9", new int[0])] // YVR not in AA-##
        [InlineData(12, 2, "0.9", new int[0])] // YVR not in AB-##
        [InlineData(12, 3, "0.9", new int[0])] // YVR not in {AA-##, AB-##}
        [InlineData(12, 4, "0.9", new[] {10711})] // 0.989
        public async Task DestinationAirportQueryBuilder_CatchmentThreshold(int eventId, int geonameId, string threshold, int[] expectedDestinationAirportIds)
        {
            // Update the threshold
            var config = new ConfigurationVariables
            {
                ConfigurationVariableId = Guid.NewGuid(),
                Name = nameof(ConfigurationVariableName.DestinationCatchmentThreshold),
                Value = threshold,
                ValueType = nameof(ConfigurationVariableType.Double)
            };
            _fixture.DbContext.ConfigurationVariables.Add(config);
            _fixture.DbContext.SaveChanges();

            // Perform the test
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).ShouldIncludeImportationRisk(geonameId).Build())
                    .BuildAndExecute())
                .ToArray();

            Assert.Equal(expectedDestinationAirportIds.Length, result.Length);
            Assert.Equal(expectedDestinationAirportIds, result.Select(a => a.StationId).OrderBy(d => d));
        }

        [Theory]
        [InlineData(999, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(11, true)]
        [InlineData(12, false)]
        public async Task DestinationAirportQueryBuilder_IsModelNotRun(int eventId, bool expectedIsModelNotRun)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .ToArray();

            Assert.All(result, a => Assert.Equal(expectedIsModelNotRun, a.IsModelNotRun));
        }


        [Theory]
        [InlineData(999, new string[0], new string[0])]
        [InlineData(1, new[] {"TPE"}, new[] {"Taiwan Taoyuan International Airport"})]
        [InlineData(2, new[] {"CDG", "TPE", "YVR"}, new[] {"Paris Charles de Gaulle Airport", "Taiwan Taoyuan International Airport", "Vancouver International Airport"})]
        [InlineData(11, new[] {"YYZ"}, new[] {"Toronto Pearson International Airport"})]
        [InlineData(12, new[] {"YVR"}, new[] {"Vancouver International Airport"})]
        public async Task DestinationAirportQueryBuilder_StationNames(int eventId, string[] expectedStationCodes, string[] expectedStationNames)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedStationCodes.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedStationCodes[i], resultStation.StationCode);
                Assert.Equal(expectedStationNames[i], resultStation.StationName);
            }
        }

        [Theory]
        [InlineData(999, new float[0], new float[0])]
        [InlineData(1, new[] {25.07773f}, new[] {121.23282f})]
        [InlineData(2, new[] {49.01278f, 25.07773f, 49.19489f}, new[] {2.55000f, 121.23282f, -123.17923f})]
        [InlineData(11, new[] {43.68066f}, new[] {-79.61286f})]
        [InlineData(12, new[] {49.19489f}, new[] {-123.17923f})]
        public async Task DestinationAirportQueryBuilder_StationLatLong(int eventId, float[] expectedLatitudes, float[] expectedLongitudes)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedLatitudes.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedLatitudes[i], resultStation.Latitude, 5); // SQL column is decimal(10, 5)
                Assert.Equal(expectedLongitudes[i], resultStation.Longitude, 5); // SQL column is decimal(10, 5)
            }
        }

        [Theory]
        [InlineData(999, new int[0])]
        [InlineData(1, new[] {235891})]
        [InlineData(2, new[] {1266754, 65437547, 5362})]
        [InlineData(11, new[] {0})]
        [InlineData(12, new[] {0})]
        public async Task DestinationAirportQueryBuilder_Volume(int eventId, int[] expectedVolumes)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedVolumes.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedVolumes[i], resultStation.Volume);
            }
        }

        [Theory]
        [InlineData(999, new float[0], new float[0])]
        [InlineData(1, new[] {0.8789f}, new[] {0.9001f})]
        [InlineData(2, new[] {0.0000f, 0.0089f, 0.2693f}, new[] {0.0440f, 0.0116f, 0.3143f})]
        [InlineData(11, new[] {0f}, new[] {0f})]
        [InlineData(12, new[] {0f}, new[] {0f})]
        public async Task DestinationAirportQueryBuilder_Probabilities(int eventId, float[] expectedMinProbabilities, float[] expectedMaxProbabilities)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedMinProbabilities.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedMinProbabilities[i], resultStation.MinProb, 4); // SQL column is decimal(5, 4)
                Assert.Equal(expectedMaxProbabilities[i], resultStation.MaxProb, 4); // SQL column is decimal(5, 4)
            }
        }

        [Theory]
        [InlineData(999, new float[0], new float[0])]
        [InlineData(1, new[] {2.111f}, new[] {2.303f})]
        [InlineData(2, new[] {750.739f, 49.387f, 9.569f}, new[] {770.134f, 50.519f, 10.892f})]
        [InlineData(11, new[] {0f}, new[] {0f})]
        [InlineData(12, new[] {0f}, new[] {0f})]
        public async Task DestinationAirportQueryBuilder_Magnitudes(int eventId, float[] expectedMinMagnitudes, float[] expectedMaxMagnitudes)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedMinMagnitudes.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedMinMagnitudes[i], resultStation.MinExpVolume, 3); // SQL column is decimal(10, 3)
                Assert.Equal(expectedMaxMagnitudes[i], resultStation.MaxExpVolume, 3); // SQL column is decimal(10, 3)
            }
        }

        [Theory]
        [MemberData(nameof(DestinationAirportQueryBuilderDatabaseFixture.CITY_NAMES_TEST_DATA), MemberType = typeof(DestinationAirportQueryBuilderDatabaseFixture))]
        public async Task DestinationAirportQueryBuilder_CityNames(int eventId, int?[] expectedCityIds, string[] expectedCityNames)
        {
            var result = (await new DestinationAirportQueryBuilder(
                        _fixture.DbContext,
                        new AirportConfig.Builder(eventId).ShouldIncludeCity().Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedCityIds.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedCityIds[i], resultStation.CityGeonameId);
                Assert.Equal(expectedCityNames[i], resultStation.CityName);
            }
        }
    }
}