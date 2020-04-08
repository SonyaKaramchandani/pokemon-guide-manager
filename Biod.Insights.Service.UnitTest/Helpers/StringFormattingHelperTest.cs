using System;
using Biod.Insights.Service.Helpers;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class StringFormattingHelperTest
    {
        #region FormatTime

        [Theory]
        [InlineData(0, "0 seconds")]
        [InlineData(1, "1 second")]
        [InlineData(59, "59 seconds")]
        [InlineData(60, "1 minute")]
        [InlineData(63, "1.1 minutes")]
        [InlineData(3596, "59.9 minutes")]
        [InlineData(3597, "60 minutes")]
        [InlineData(3600, "1 hour")]
        [InlineData(3780, "1.1 hours")]
        [InlineData(86219, "23.9 hours")]
        [InlineData(86220, "24 hours")]
        [InlineData(86400, "1 day")]
        [InlineData(90720, "1.1 days")]
        [InlineData(31531679, "364.9 days")]
        [InlineData(31531680, "365 days")]
        [InlineData(31536000, "1 year")]
        [InlineData(33112800, "1.1 years")]
        [InlineData(126144000, "4 years")]
        public void FormatTime(long seconds, string expectedResult)
        {
            var result = StringFormattingHelper.FormatTime(seconds);
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region FormatPeriod

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, 0L, null)]
        [InlineData(null, 5L, null)]
        [InlineData(0L, null, null)]
        [InlineData(5L, null, null)]
        [InlineData(0L, 0L, null)]
        [InlineData(5L, 0L, null)]
        public void FormatPeriod_Null(long? minSeconds, long? maxSeconds, long? avgSeconds)
        {
            var result = StringFormattingHelper.FormatPeriod(minSeconds, maxSeconds, avgSeconds);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(null, null, 10L)]
        [InlineData(null, 0L, 10L)]
        [InlineData(null, 5L, 10L)]
        [InlineData(0L, null, 10L)]
        [InlineData(5L, null, 10L)]
        [InlineData(0L, 0L, 10L)]
        [InlineData(5L, 0L, 10L)]
        public void FormatPeriod_AverageOnly(long? minSeconds, long? maxSeconds, long? avgSeconds)
        {
            var result = StringFormattingHelper.FormatPeriod(minSeconds, maxSeconds, avgSeconds);
            Assert.Equal("10 seconds average", result);
        }

        [Fact]
        public void FormatPeriod_RangeOnly()
        {
            var result = StringFormattingHelper.FormatPeriod(10L, 38L, null);
            Assert.Equal("10 seconds to 38 seconds", result);
        }

        [Fact]
        public void FormatPeriod_RangeAndAverage()
        {
            var result = StringFormattingHelper.FormatPeriod(10L, 38L, 22L);
            Assert.Equal("10 seconds to 38 seconds (22 seconds avg.)", result);
        }

        #endregion

        #region FormatDateWithConditionalYear

        [Fact]
        public void FormatDateWithConditionalYear_Null()
        {
            var result = StringFormattingHelper.FormatDateWithConditionalYear(null);
            Assert.Equal("", result);
        }

        [Theory]
        [InlineData(3, 1, "March 1")]
        [InlineData(2, 28, "February 28")]
        [InlineData(8, 15, "August 15")]
        [InlineData(12, 31, "December 31")]
        public void FormatDateWithConditionalYear_SameYear(int month, int day, string expectedResult)
        {
            var input = new DateTime(DateTime.Now.Year, month, day);
            var result = StringFormattingHelper.FormatDateWithConditionalYear(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(3, 1, 1994, "March 1, 1994")]
        [InlineData(2, 28, 2002, "February 28, 2002")]
        [InlineData(8, 15, 2015, "August 15, 2015")]
        [InlineData(12, 31, 2019, "December 31, 2019")]
        public void FormatDateWithConditionalYear_DifferentYear(int month, int day, int year, string expectedResult)
        {
            var input = new DateTime(year, month, day);
            var result = StringFormattingHelper.FormatDateWithConditionalYear(input);
            Assert.Equal(expectedResult, result);
        }

        #endregion
    }
}