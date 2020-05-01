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
    public class GeonameQueryBuilderTest : IClassFixture<GeonameQueryBuilderDatabaseFixture>
    {
        private readonly GeonameQueryBuilderDatabaseFixture _fixture;

        public GeonameQueryBuilderTest(GeonameQueryBuilderDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GeonameQueryBuilder_OverrideInitialQueryable()
        {
            var queryable = new List<Geonames>().AsQueryable();
            var queryBuilder = new GeonameQueryBuilder(_fixture.DbContext).OverrideInitialQueryable(queryable);
            Assert.Equal(queryable, queryBuilder.GetInitialQueryable());
        }

        [Fact]
        public async Task GeonameQueryBuilder_NoConfig()
        {
            var result = await new GeonameQueryBuilder(_fixture.DbContext).BuildAndExecute();
            Assert.Equal(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, result.Select(g => g.Id).OrderBy(d => d));
        }

        [Fact]
        public async Task GeonameQueryBuilder_SingleGeoname()
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(1)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Single(result);
            Assert.Equal(1, result.Single().Id);
        }

        [Fact]
        public async Task GeonameQueryBuilder_NonExistentGeoname()
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(999)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(new int[0], new[] {1, 2, 3, 4, 5, 6, 7, 8, 9})]
        [InlineData(new[] {2}, new[] {2})]
        [InlineData(new[] {1, 3, 5, 7}, new[] {1, 3, 5, 7})]
        public async Task GeonameQueryBuilder_GeonameIds(int[] geonameIds, int[] expectedGeonameIds)
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameIds(geonameIds)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedGeonameIds, result.Select(g => g.Id).OrderBy(d => d));
        }

        [Theory]
        [InlineData(1, "Canada", "Canada")]
        [InlineData(2, "United States of America", "United States")]
        [InlineData(3, "British Columbia", "British Columbia, Canada")]
        [InlineData(6, "Vancouver", "Vancouver, British Columbia, Canada")]
        [InlineData(9, "Mars", "Crater Lake, Mars")]
        public async Task GeonameQueryBuilder_GeonameNames(int geonameId, string expectedGeonameName, string expectedGeonameDisplayName)
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(geonameId)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedGeonameName, result.Single().Name);
            Assert.Equal(expectedGeonameDisplayName, result.Single().DisplayName);
        }

        [Theory]
        [InlineData(1, (int) LocationType.Country)]
        [InlineData(3, (int) LocationType.Province)]
        [InlineData(6, (int) LocationType.City)]
        [InlineData(9, (int) LocationType.Unknown)]
        public async Task GeonameQueryBuilder_LocationType(int geonameId, int expectedLocationType)
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(geonameId)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedLocationType, result.Single().LocationType);
        }

        [Theory]
        [InlineData(1, "Canada")]
        [InlineData(3, "Canada")]
        [InlineData(8, "United States")]
        [InlineData(9, null)]
        public async Task GeonameQueryBuilder_CountryName(int geonameId, string expectedCountryName)
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(geonameId)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedCountryName, result.Single().CountryName);
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(3, "British Columbia")]
        [InlineData(8, "Washington")]
        [InlineData(9, null)]
        public async Task GeonameQueryBuilder_ProvinceName(int geonameId, string expectedProvinceName)
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(geonameId)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedProvinceName, result.Single().ProvinceName);
        }

        [Theory]
        [InlineData(1, 60.10867f, -113.64258f)]
        [InlineData(3, 53.99983f, -125.00320f)]
        [InlineData(8, 47.60621f, -122.33207f)]
        [InlineData(9, 0f, 0f)]
        public async Task GeonameQueryBuilder_LatLong(int geonameId, float latitude, float longitude)
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(geonameId)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(latitude, result.Single().Latitude, 5); // SQL column is decimal(10, 5)
            Assert.Equal(longitude, result.Single().Longitude, 5); // SQL column is decimal(10, 5)
        }

        [Theory]
        [InlineData(1, "Shape Text for Canada")]
        [InlineData(3, "Shape Text for British Columbia")]
        [InlineData(8, null)]
        [InlineData(9, null)]
        public async Task GeonameQueryBuilder_Shape(int geonameId, string expectedShape)
        {
            var result = (await new GeonameQueryBuilder(_fixture.DbContext, new GeonameConfig.Builder()
                    .AddGeonameId(geonameId)
                    .ShouldIncludeShape()
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedShape, result.Single().ShapeAsText);
        }
    }
}