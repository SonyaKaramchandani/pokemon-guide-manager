using System;
using System.Linq;
using Biod.Insights.Service.Configs;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Configs
{
    public class DiseaseConfigTest
    {
        [Fact]
        public void DiseaseConfigBuilder_Default()
        {
            var config = new DiseaseConfig.Builder().Build();

            Assert.Empty(config.DiseaseIds);
            Assert.False(config.IncludeAcquisitionModes);
            Assert.False(config.IncludeAgents);
            Assert.False(config.IncludeBiosecurityRisks);
            Assert.False(config.IncludeIncubationPeriods);
            Assert.False(config.IncludeInterventions);
            Assert.False(config.IncludeSymptomaticPeriods);
            Assert.False(config.IncludeTransmissionModes);
        }

        [Fact]
        public void DiseaseConfigBuilder_DiseaseId()
        {
            var diseaseId = new Random().Next(0, int.MaxValue);
            var config = new DiseaseConfig.Builder()
                .AddDiseaseId(diseaseId)
                .Build();
            Assert.Contains(diseaseId, config.DiseaseIds);
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new[] {1})]
        [InlineData(new[] {414, 213, 513, 32, 62, 1508})]
        public void DiseaseConfigBuilder_DiseaseIds(int[] diseaseIds)
        {
            var config = new DiseaseConfig.Builder()
                .AddDiseaseIds(diseaseIds)
                .Build();
            Assert.Equal(diseaseIds.OrderBy(d => d), config.DiseaseIds.OrderBy(d => d));
        }

        [Fact]
        public void DiseaseConfigBuilder_SingleDiseaseId()
        {
            var config = new DiseaseConfig.Builder()
                .AddDiseaseIds(new[] {123})
                .Build();

            Assert.Equal(123, config.GetDiseaseId());
        }

        [Fact]
        public void DiseaseConfigBuilder_SingleDiseaseIdException()
        {
            var config = new DiseaseConfig.Builder()
                .AddDiseaseIds(new[] {1, 2, 3})
                .Build();

            Assert.Throws<InvalidOperationException>(() => config.GetDiseaseId());
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeAcquisitionModes()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeAcquisitionModes()
                .Build();

            Assert.True(config.IncludeAcquisitionModes);
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeAgents()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeAgents()
                .Build();

            Assert.True(config.IncludeAgents);
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeBiosecurityRisks()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeBiosecurityRisks()
                .Build();

            Assert.True(config.IncludeBiosecurityRisks);
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeIncubationPeriods()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeIncubationPeriods()
                .Build();

            Assert.True(config.IncludeIncubationPeriods);
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeInterventions()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeInterventions()
                .Build();

            Assert.True(config.IncludeInterventions);
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeSymptomaticPeriods()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeSymptomaticPeriods()
                .Build();

            Assert.True(config.IncludeSymptomaticPeriods);
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeTransmissionModes()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeTransmissionModes()
                .Build();

            Assert.True(config.IncludeTransmissionModes);
        }

        [Fact]
        public void DiseaseConfigBuilder_IncludeAllProperties()
        {
            var config = new DiseaseConfig.Builder()
                .ShouldIncludeAllProperties()
                .Build();

            Assert.True(config.IncludeAcquisitionModes);
            Assert.True(config.IncludeAgents);
            Assert.True(config.IncludeBiosecurityRisks);
            Assert.True(config.IncludeIncubationPeriods);
            Assert.True(config.IncludeInterventions);
            Assert.True(config.IncludeSymptomaticPeriods);
            Assert.True(config.IncludeTransmissionModes);
        }
    }
}