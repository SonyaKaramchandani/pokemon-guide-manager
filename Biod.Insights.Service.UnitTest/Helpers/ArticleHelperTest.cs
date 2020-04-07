using Biod.Insights.Service.Helpers;
using Xunit;

namespace Biod.Insights.UnitTest.Service.Helpers
{
    public class ArticleHelperTest
    {
        [Theory]
        [InlineData(3, "cdc.gov", null)]
        [InlineData(3, "http://cdc.gov", null)]
        [InlineData(3, "www.cdc.gov/abc", null)]
        [InlineData(9, "wwwnc.cdc.gov", null)]
        [InlineData(9, "http://wwwnc.cdc.gov", null)]
        [InlineData(9, "wwwnc.cdc.gov/abc", null)]
        public void GetDisplayName_CDC(int? feedId, string sourceUrl, string feedDisplayName)
        {
            var result = ArticleHelper.GetDisplayName(feedId, sourceUrl, feedDisplayName);
            Assert.Equal("CDC", result);
        }
    }
}