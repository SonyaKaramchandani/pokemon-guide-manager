using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class SourceAirportQueryBuilderTest : IClassFixture<SourceAirportQueryBuilderDatabaseFixture>
    {
        private readonly SourceAirportQueryBuilderDatabaseFixture _fixture;

        public SourceAirportQueryBuilderTest(SourceAirportQueryBuilderDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void SourceAirportQueryBuilder_OverrideInitialQueryable()
        {
            var queryable = new List<EventSourceAirportSpreadMd>().AsQueryable();
            var queryBuilder = new SourceAirportQueryBuilder(
                    _fixture.DbContext,
                    new SourceAirportConfig.Builder(1).Build())
                .OverrideInitialQueryable(queryable);
            Assert.Equal(queryable, queryBuilder.GetInitialQueryable());
        }

        [Theory]
        [InlineData(999, new int[0])]
        [InlineData(1, new[] {9968})]
        [InlineData(2, new[] {7740, 9968, 10711})]
        [InlineData(11, new[] {10357})]
        [InlineData(12, new[] {10711})]
        public async Task SourceAirportQueryBuilder_StationIds(int eventId, int[] expectedSourceAirportIds)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .ToArray();

            Assert.Equal(expectedSourceAirportIds.Length, result.Length);
            Assert.Equal(expectedSourceAirportIds, result.Select(a => a.StationId).OrderBy(d => d));
        }

        [Theory]
        [InlineData(999, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(11, true)]
        [InlineData(12, true)]
        public async Task SourceAirportQueryBuilder_IsModelNotRun(int eventId, bool expectedIsModelNotRun)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
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
        public async Task SourceAirportQueryBuilder_StationNames(int eventId, string[] expectedStationCodes, string[] expectedStationNames)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
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
        public async Task SourceAirportQueryBuilder_StationLatLong(int eventId, float[] expectedLatitudes, float[] expectedLongitudes)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
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
        public async Task SourceAirportQueryBuilder_Volume(int eventId, int[] expectedVolumes)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
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
        public async Task SourceAirportQueryBuilder_Probabilities(int eventId, float[] expectedMinProbabilities, float[] expectedMaxProbabilities)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
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
        public async Task SourceAirportQueryBuilder_Magnitudes(int eventId, float[] expectedMinMagnitudes, float[] expectedMaxMagnitudes)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
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
        [InlineData(999, new double[0], new double[0])]
        [InlineData(1, new[] {0.000324118559945606d}, new[] {0.000331892011424378d})]
        [InlineData(2, new[] {0.000092679626437237d, 0.000437885515885104d, 0.000000570776679617062d}, new[] {0.000103863727029108d, 0.000449198354277068d, 0.000000844799627952716d})]
        [InlineData(11, new[] {0d}, new[] {0d})]
        [InlineData(12, new[] {0d}, new[] {0d})]
        public async Task SourceAirportQueryBuilder_Prevalence(int eventId, double[] expectedMinPrevalence, double[] expectedMaxPrevalence)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedMinPrevalence.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedMinPrevalence[i], resultStation.MinPrevalence);
                Assert.Equal(expectedMaxPrevalence[i], resultStation.MaxPrevalence);
            }
        }

        [Theory]
        [MemberData(nameof(SourceAirportQueryBuilderDatabaseFixture.CITY_NAMES_TEST_DATA), MemberType = typeof(SourceAirportQueryBuilderDatabaseFixture))]
        public async Task SourceAirportQueryBuilder_CityNames(int eventId, int?[] expectedCityIds, string[] expectedCityNames)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).ShouldIncludeCity().Build())
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

        [Theory]
        [InlineData(999, new int[0])]
        [InlineData(1, new[] {188321562})]
        [InlineData(2, new[] {0, 188321562, 14244})]
        [InlineData(11, new[] {647436})]
        [InlineData(12, new[] {14244})]
        public async Task SourceAirportQueryBuilder_WeightedPopulation(int eventId, int[] expectedPopulation)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).ShouldIncludePopulation().Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedPopulation.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultStation = result[i];
                Assert.Equal(expectedPopulation[i], resultStation.Population);
            }
        }

        [Theory]
        [MemberData(nameof(SourceAirportQueryBuilderDatabaseFixture.CASE_COUNTS_TEST_DATA), MemberType = typeof(SourceAirportQueryBuilderDatabaseFixture))]
        public async Task SourceAirportQueryBuilder_CaseCounts(int eventId, GridStationCaseJoinResult[][] expectedGridStationCases)
        {
            var result = (await new SourceAirportQueryBuilder(
                        _fixture.DbContext,
                        new SourceAirportConfig.Builder(eventId).ShouldIncludeCaseCounts().Build())
                    .BuildAndExecute())
                .OrderBy(r => r.StationId)
                .ToArray();

            Assert.Equal(expectedGridStationCases.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
            {
                var resultGridCases = result[i].GridStationCases
                    .OrderBy(c => c.Probability)
                    .ThenBy(c => c.Cases)
                    .ToArray();
                var expectedGridCases = expectedGridStationCases[i]
                    .OrderBy(c => c.Probability)
                    .ThenBy(c => c.Cases)
                    .ToArray();

                Assert.Equal(expectedGridCases.Length, resultGridCases.Length);
                for (var j = 0; j < resultGridCases.Length; j++)
                {
                    Assert.Equal(expectedGridCases[j].Probability, resultGridCases[j].Probability, 8); // SQL column is decimal(10, 8)
                    Assert.Equal(expectedGridCases[j].Cases, resultGridCases[j].Cases);
                    Assert.Equal(expectedGridCases[j].MinCases, resultGridCases[j].MinCases);
                    Assert.Equal(expectedGridCases[j].MaxCases, resultGridCases[j].MaxCases);
                }
            }
        }
    }
}