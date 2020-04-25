using Biod.Insights.Service.Helpers;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class ArticleHelperTest
    {
        #region GetDisplayName

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

        [Theory]
        [InlineData(9, "ecdc.europa.eu", null)]
        [InlineData(9, "http://ecdc.europa.eu", null)]
        [InlineData(9, "ecdc.europa.eu/abc", null)]
        public void GetDisplayName_ECDC(int? feedId, string sourceUrl, string feedDisplayName)
        {
            var result = ArticleHelper.GetDisplayName(feedId, sourceUrl, feedDisplayName);
            Assert.Equal("ECDC", result);
        }

        [Theory]
        [InlineData(9, "chp.gov.hk", null)]
        [InlineData(9, "http://chp.gov.hk", null)]
        [InlineData(9, "chp.gov.hk/abc", null)]
        public void GetDisplayName_OtherOfficial(int? feedId, string sourceUrl, string feedDisplayName)
        {
            var result = ArticleHelper.GetDisplayName(feedId, sourceUrl, feedDisplayName);
            Assert.Equal("Other Official", result);
        }

        [Theory]
        [InlineData(3, null, null)]
        [InlineData(3, "cdc2.gov", null)]
        [InlineData(3, "abc", null)]
        [InlineData(9, null, null)]
        [InlineData(9, "wwwnc2.cdc.gov", null)]
        [InlineData(9, "ecdc2.europa.eu", null)]
        [InlineData(9, "chp2.gov.hk", null)]
        [InlineData(9, "abc", null)]
        public void GetDisplayName_NewsMedia(int? feedId, string sourceUrl, string feedDisplayName)
        {
            var result = ArticleHelper.GetDisplayName(feedId, sourceUrl, feedDisplayName);
            Assert.Equal("News Media", result);
        }

        [Theory]
        [InlineData(0, null, null)]
        [InlineData(1, "cdc.gov", null)]
        [InlineData(2, "cdc.gov", null)]
        [InlineData(4, "cdc.gov", null)]
        [InlineData(5, "cdc.gov", null)]
        [InlineData(6, "cdc.gov", null)]
        [InlineData(7, "cdc.gov", null)]
        [InlineData(8, "cdc.gov", null)]
        [InlineData(10, "cdc.gov", null)]
        public void GetDisplayName_Empty(int? feedId, string sourceUrl, string feedDisplayName)
        {
            var result = ArticleHelper.GetDisplayName(feedId, sourceUrl, feedDisplayName);
            Assert.Equal("", result);
        }

        [Theory]
        [InlineData(0, null, "null")]
        [InlineData(1, "cdc.gov", "ABC")]
        [InlineData(2, "cdc.gov", "Some website")]
        [InlineData(4, "cdc.gov", "Twitter")]
        [InlineData(5, "cdc.gov", "Facebook News")]
        [InlineData(6, "cdc.gov", "FakeNews.com")]
        [InlineData(7, "cdc.gov", "NotFakeNews.com")]
        [InlineData(8, "cdc.gov", "123456")]
        [InlineData(10, "cdc.gov", "cdc.gov")]
        public void GetDisplayName_FeedName(int? feedId, string sourceUrl, string feedDisplayName)
        {
            var result = ArticleHelper.GetDisplayName(feedId, sourceUrl, feedDisplayName);
            Assert.Equal(feedDisplayName, result);
        }

        #endregion

        #region GetSeqId

        [Theory]
        [InlineData(3, "cdc.gov", null)]
        [InlineData(3, "http://cdc.gov", null)]
        [InlineData(3, "www.cdc.gov/abc", null)]
        [InlineData(9, "wwwnc.cdc.gov", null)]
        [InlineData(9, "http://wwwnc.cdc.gov", null)]
        [InlineData(9, "wwwnc.cdc.gov/abc", null)]
        public void GetSeqId_2(int? feedId, string sourceUrl, int? seqId)
        {
            var result = ArticleHelper.GetSeqId(feedId, sourceUrl, seqId);
            Assert.Equal(2, result);
        }

        [Theory]
        [InlineData(9, null, null)]
        [InlineData(9, "wwwnc2.cdc.gov", null)]
        [InlineData(9, "http://wwwnc2.cdc.gov", null)]
        [InlineData(9, "wwwnc2.cdc.gov/abc", null)]
        public void GetSeqId_6(int? feedId, string sourceUrl, int? seqId)
        {
            var result = ArticleHelper.GetSeqId(feedId, sourceUrl, seqId);
            Assert.Equal(6, result);
        }

        [Theory]
        [InlineData(3, null, null)]
        [InlineData(3, "cdc2.gov", null)]
        [InlineData(3, "http://cdc2.gov", null)]
        [InlineData(3, "cdc2.gov/abc", null)]
        public void GetSeqId_7(int? feedId, string sourceUrl, int? seqId)
        {
            var result = ArticleHelper.GetSeqId(feedId, sourceUrl, seqId);
            Assert.Equal(7, result);
        }

        [Theory]
        [InlineData(0, null, null)]
        [InlineData(1, "cdc.gov", null)]
        [InlineData(2, "cdc.gov", null)]
        [InlineData(4, "cdc.gov", null)]
        [InlineData(5, "cdc.gov", null)]
        [InlineData(6, "cdc.gov", null)]
        [InlineData(7, "cdc.gov", null)]
        [InlineData(8, "cdc.gov", null)]
        [InlineData(10, "cdc.gov", null)]
        public void GetSeqId_0(int? feedId, string sourceUrl, int? seqId)
        {
            var result = ArticleHelper.GetSeqId(feedId, sourceUrl, seqId);
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(0, null, 0)]
        [InlineData(1, "cdc.gov", 1)]
        [InlineData(2, "cdc.gov", 2)]
        [InlineData(4, "cdc.gov", 4)]
        [InlineData(5, "cdc.gov", 5)]
        [InlineData(6, "cdc.gov", 6)]
        [InlineData(7, "cdc.gov", 7)]
        [InlineData(8, "cdc.gov", 8)]
        [InlineData(10, "cdc.gov", 10)]
        public void GetSeqId_SeqId(int? feedId, string sourceUrl, int? seqId)
        {
            var result = ArticleHelper.GetSeqId(feedId, sourceUrl, seqId);
            Assert.Equal(seqId, result);
        }

        #endregion
    }
}