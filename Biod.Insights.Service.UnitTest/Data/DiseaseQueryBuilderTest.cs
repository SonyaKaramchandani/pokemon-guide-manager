using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Models.Disease;
using Xunit;
using OutbreakPotentialCategory = Biod.Products.Common.Constants.OutbreakPotentialCategory;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class DiseaseQueryBuilderTest : IClassFixture<DiseaseQueryBuilderDatabaseFixture>
    {
        private readonly DiseaseQueryBuilderDatabaseFixture _fixture;

        public DiseaseQueryBuilderTest(DiseaseQueryBuilderDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void DiseaseQueryBuilder_OverrideInitialQueryable()
        {
            var queryable = new List<Diseases>().AsQueryable();
            var queryBuilder = new DiseaseQueryBuilder(_fixture.DbContext).OverrideInitialQueryable(queryable);
            Assert.Equal(queryable, queryBuilder.GetInitialQueryable());
        }

        [Fact]
        public async Task DiseaseQueryBuilder_NoConfig()
        {
            var result = await new DiseaseQueryBuilder(_fixture.DbContext).BuildAndExecute();
            Assert.Equal(new[] {1, 2, 3, 4, 5}, result.Select(d => d.DiseaseId).OrderBy(d => d));
        }

        [Fact]
        public async Task DiseaseQueryBuilder_SingleDisease()
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(1)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Single(result);
            Assert.Equal(1, result.Single().DiseaseId);
        }

        [Fact]
        public async Task DiseaseQueryBuilder_NonExistentDisease()
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(999)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(new int[0], new[] {1, 2, 3, 4, 5})]
        [InlineData(new[] {2}, new[] {2})]
        [InlineData(new[] {1, 3, 5}, new[] {1, 3, 5})]
        public async Task DiseaseQueryBuilder_DiseasesIds(int[] diseaseIds, int[] expectedDiseaseIds)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseIds(diseaseIds)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedDiseaseIds, result.Select(d => d.DiseaseId).OrderBy(d => d));
        }

        [Theory]
        [InlineData(1, "Apple")]
        [InlineData(3, "Carrot")]
        [InlineData(5, "Egg")]
        public async Task DiseaseQueryBuilder_DiseaseName(int diseaseId, string expectedDiseaseName)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedDiseaseName, result.Single().DiseaseName);
        }

        [Theory]
        [InlineData(1, (int) OutbreakPotentialCategory.Unknown)]
        [InlineData(2, (int) OutbreakPotentialCategory.Sporadic)]
        [InlineData(3, (int) OutbreakPotentialCategory.NeedsMapSustained)]
        [InlineData(5, (int) OutbreakPotentialCategory.Unknown)]
        public async Task DiseaseQueryBuilder_OutbreakPotentialAttributeId(int diseaseId, int expectedOutbreakPotentialAttributeId)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedOutbreakPotentialAttributeId, result.Single().OutbreakPotentialAttributeId);
        }

        [Theory]
        [InlineData(1, new string[0])]
        [InlineData(2, new string[0])]
        [InlineData(3, new[] {"Agent 859723"})]
        [InlineData(4, new[] {"Agent 423123", "Agent 234897", "Agent 239034", "Agent 697208", "Agent 462234"})]
        public async Task DiseaseQueryBuilder_Agents(int diseaseId, string[] expectedAgentNames)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeAgents()
                    .Build()).BuildAndExecute())
                .ToList();
            Assert.Equal(expectedAgentNames.OrderBy(n => n), result.Single().Agents.OrderBy(a => a));
        }

        [Theory]
        [InlineData(1, new string[0])]
        [InlineData(2, new string[0])]
        [InlineData(3, new[] {"Agent Type 123124"})]
        [InlineData(4, new[] {"Agent Type 123124", "Agent Type 559832"})]
        public async Task DiseaseQueryBuilder_AgentTypes(int diseaseId, string[] expectedAgentTypes)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeAgents()
                    .Build()).BuildAndExecute())
                .ToList();
            var resultAgentTypes = result.Single().AgentTypes.ToArray();

            Assert.Equal(expectedAgentTypes.Length, resultAgentTypes.Length);
            foreach (var item in expectedAgentTypes.Select((value, i) => new {i, value}))
            {
                Assert.Equal(item.value, resultAgentTypes[item.i]);
            }
        }

        [Theory]
        [MemberData(nameof(DiseaseQueryBuilderDatabaseFixture.AcquisitionModesTestData), MemberType = typeof(DiseaseQueryBuilderDatabaseFixture))]
        public async Task DiseaseQueryBuilder_AcquisitionModes(int diseaseId, AcquisitionModeModel[] expectedAcquisitionModes)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeAcquisitionModes()
                    .Build()).BuildAndExecute())
                .ToList();
            var resultAcquisitionModes = result.Single().AcquisitionModes.ToArray();

            Assert.Equal(expectedAcquisitionModes.Length, resultAcquisitionModes.Length);
            foreach (var item in expectedAcquisitionModes.Select((value, i) => new {i, value}))
            {
                var resultItem = resultAcquisitionModes[item.i];
                Assert.Equal(item.value.Id, resultItem.Id);
                Assert.Equal(item.value.Label, resultItem.Label);
                Assert.Equal(item.value.Description, resultItem.Description);
                Assert.Equal(item.value.RankId, resultItem.RankId);
                Assert.Equal(item.value.ModalityId, resultItem.ModalityId);
                Assert.Equal(item.value.ModalityName, resultItem.ModalityName);
                Assert.Equal(item.value.VectorId, resultItem.VectorId);
                Assert.Equal(item.value.VectorName, resultItem.VectorName);
            }
        }

        [Theory]
        [InlineData(1, new string[0])]
        [InlineData(2, new string[0])]
        [InlineData(3, new string[0])]
        [InlineData(4, new[] {"Transmission Mode 964836"})]
        [InlineData(5, new[] {"Transmission Mode 212452", "Transmission Mode 348729", "Transmission Mode 964836"})]
        public async Task DiseaseQueryBuilder_TransmissionModes(int diseaseId, string[] expectedTransmissionModes)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeTransmissionModes()
                    .Build()).BuildAndExecute())
                .ToList();
            var resultTransmissionModes = result.Single().TransmissionModes.ToArray();

            Assert.Equal(expectedTransmissionModes.Length, resultTransmissionModes.Length);
            foreach (var item in expectedTransmissionModes.Select((value, i) => new {i, value}))
            {
                Assert.Equal(item.value, resultTransmissionModes[item.i]);
            }
        }

        [Theory]
        [InlineData(1, new string[0])]
        [InlineData(2, new string[0])]
        [InlineData(3, new string[0])]
        [InlineData(4, new[] {"Prevention Measure 1283975"})]
        [InlineData(5, new[] {"Prevention Measure 2349812", "Prevention Measure 3145515", "Prevention Measure 4608920"})]
        public async Task DiseaseQueryBuilder_PreventionMeasures(int diseaseId, string[] expectedPreventionMeasures)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeInterventions()
                    .Build()).BuildAndExecute())
                .ToList();
            var resultPreventionMeasures = result.Single().PreventionMeasures.ToArray();

            Assert.Equal(expectedPreventionMeasures.Length, resultPreventionMeasures.Length);
            foreach (var item in expectedPreventionMeasures.Select((value, i) => new {i, value}))
            {
                Assert.Equal(item.value, resultPreventionMeasures[item.i]);
            }
        }

        [Theory]
        [InlineData(1, null, null, null)]
        [InlineData(2, null, null, null)]
        [InlineData(3, null, null, null)]
        [InlineData(4, 123, 567, 424)]
        public async Task DiseaseQueryBuilder_IncubationPeriod(int diseaseId, long? min, long? max, long? avg)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeIncubationPeriods()
                    .Build()).BuildAndExecute())
                .ToList();
            var resultIncubationPeriod = result.Single().IncubationPeriod;

            Assert.Equal(min, resultIncubationPeriod?.IncubationMinimumSeconds);
            Assert.Equal(max, resultIncubationPeriod?.IncubationMaximumSeconds);
            Assert.Equal(avg, resultIncubationPeriod?.IncubationAverageSeconds);
        }

        [Theory]
        [InlineData(1, null, null, null)]
        [InlineData(2, null, null, null)]
        [InlineData(3, null, null, null)]
        [InlineData(4, 123, 567, 424)]
        public async Task DiseaseQueryBuilder_SymptomaticPeriod(int diseaseId, long? min, long? max, long? avg)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeSymptomaticPeriods()
                    .Build()).BuildAndExecute())
                .ToList();
            var resultSymptomaticPeriod = result.Single().SymptomaticPeriod;

            Assert.Equal(min, resultSymptomaticPeriod?.SymptomaticMinimumSeconds);
            Assert.Equal(max, resultSymptomaticPeriod?.SymptomaticMaximumSeconds);
            Assert.Equal(avg, resultSymptomaticPeriod?.SymptomaticAverageSeconds);
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(2, null)]
        [InlineData(3, null)]
        [InlineData(4, "Biosecurity Risk 6938983")]
        public async Task DiseaseQueryBuilder_BiosecurityRisk(int diseaseId, string expectedBiosecurityRisk)
        {
            var result = (await new DiseaseQueryBuilder(_fixture.DbContext, new DiseaseConfig.Builder()
                    .AddDiseaseId(diseaseId)
                    .ShouldIncludeBiosecurityRisks()
                    .Build()).BuildAndExecute())
                .ToList();

            Assert.Equal(expectedBiosecurityRisk, result.Single().BiosecurityRisk);
        }
    }
}