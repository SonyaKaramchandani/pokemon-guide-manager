using Biod.Insights.Common.Constants;
using Biod.Insights.Service.Helpers;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class OutbreakPotentialCategoryHelperTest
    {
        #region GetOutbreakPotentialCategory

        [Theory]
        [InlineData(1, true)]
        [InlineData(1, false)]
        [InlineData(3, true)]
        public void GetOutbreakPotentialCategory_Sustained(int attributeId, bool hasMapThresholdRisk)
        {
            var result = OutbreakPotentialCategoryHelper.GetOutbreakPotentialCategory(attributeId, hasMapThresholdRisk);
            Assert.Equal((int) OutbreakPotentialCategory.Sustained, result.Id);
            Assert.Equal("Sustained", result.Name);
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(2, false)]
        public void GetOutbreakPotentialCategory_Sporadic(int attributeId, bool hasMapThresholdRisk)
        {
            var result = OutbreakPotentialCategoryHelper.GetOutbreakPotentialCategory(attributeId, hasMapThresholdRisk);
            Assert.Equal((int) OutbreakPotentialCategory.Sporadic, result.Id);
            Assert.Equal("Sporadic", result.Name);
        }

        [Theory]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(4, false)]
        public void GetOutbreakPotentialCategory_Unlikely(int attributeId, bool hasMapThresholdRisk)
        {
            var result = OutbreakPotentialCategoryHelper.GetOutbreakPotentialCategory(attributeId, hasMapThresholdRisk);
            Assert.Equal((int) OutbreakPotentialCategory.Unlikely, result.Id);
            Assert.Equal("Negligible or none", result.Name);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(0, false)]
        [InlineData(5, true)]
        [InlineData(5, false)]
        public void GetOutbreakPotentialCategory_Unknown(int attributeId, bool hasMapThresholdRisk)
        {
            var result = OutbreakPotentialCategoryHelper.GetOutbreakPotentialCategory(attributeId, hasMapThresholdRisk);
            Assert.Equal((int) OutbreakPotentialCategory.Unknown, result.Id);
            Assert.Equal("Unknown", result.Name);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(4, false)]
        [InlineData(5, true)]
        [InlineData(5, false)]
        public void GetOutbreakPotentialCategory_AttributeId(int attributeId, bool hasMapThresholdRisk)
        {
            var result = OutbreakPotentialCategoryHelper.GetOutbreakPotentialCategory(attributeId, hasMapThresholdRisk);
            Assert.Equal(attributeId, result.AttributeId);
        }

        #endregion

        #region IsMapNeeded

        [Theory]
        [InlineData(3)]
        public void IsMapNeeded_True(int attributeId)
        {
            var result = OutbreakPotentialCategoryHelper.IsMapNeeded(attributeId);
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(5)]
        public void IsMapNeeded_False(int attributeId)
        {
            var result = OutbreakPotentialCategoryHelper.IsMapNeeded(attributeId);
            Assert.False(result);
        }

        #endregion
    }
}