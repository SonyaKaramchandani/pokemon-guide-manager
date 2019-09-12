using System;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest.Infrastructures
{
    [TestClass]
    public class DateHelperTest
    {
        [TestMethod]
        public void TestParseStartDateString_Null()
        {
            var parsedDate = DateHelper.ParseStartDateString(null);
            Assert.IsNull(parsedDate, "Null string should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_EmptyString()
        {
            var parsedDate = DateHelper.ParseStartDateString("");
            Assert.IsNull(parsedDate, "Empty string should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_PartialTime()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-01-01T00:22:");
            Assert.IsNull(parsedDate, "Incomplete time should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_InvalidMonth()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-13-31T08:29:11");
            Assert.IsNull(parsedDate, "Invalid month should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_InvalidDay()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-10-32T08:29:11");
            Assert.IsNull(parsedDate, "Invalid day should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_InvalidHour()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-10-31T38:29:11");
            Assert.IsNull(parsedDate, "Invalid hour should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_InvalidMinute()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-10-30T08:99:11");
            Assert.IsNull(parsedDate, "Invalid minute should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_InvalidSecond()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-10-01T08:29:60");
            Assert.IsNull(parsedDate, "Invalid second should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_InvalidSeparator()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-10-29_08:29:11");
            Assert.IsNull(parsedDate, "Invalid date-time separator should return a null date");
        }

        [TestMethod]
        public void TestParseStartDateString_YearOnly()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 1, 1, 0, 0, 0, 0, TimeSpan.Zero), parsedDate, "Year-only date should only contain year");
        }

        [TestMethod]
        public void TestParseStartDateString_YearAndMonthOnly()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-03");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 3, 1, 0, 0, 0, 0, TimeSpan.Zero), parsedDate, "Year and month date should only contain year and month");
        }

        [TestMethod]
        public void TestParseStartDateString_DateOnly()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-03-31");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 3, 31, 0, 0, 0, 0, TimeSpan.Zero), parsedDate, "Date should only contain year, month, and day");
        }

        [TestMethod]
        public void TestParseStartDateString_DateTime()
        {
            var parsedDate = DateHelper.ParseStartDateString("2019-03-31T08:29:11");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 3, 31, 8, 29, 11, 0, TimeSpan.Zero), parsedDate, "Full Date Time not in the resulting date time");
        }

        [TestMethod]
        public void TestParseEndDateString_Null()
        {
            var parsedDate = DateHelper.ParseEndDateString(null);
            Assert.IsNull(parsedDate, "Null string should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_EmptyString()
        {
            var parsedDate = DateHelper.ParseEndDateString("");
            Assert.IsNull(parsedDate, "Empty string should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_PartialTime()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-01-01T00:22:");
            Assert.IsNull(parsedDate, "Incomplete time should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_InvalidMonth()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-13-31T08:29:11");
            Assert.IsNull(parsedDate, "Invalid month should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_InvalidDay()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-10-32T08:29:11");
            Assert.IsNull(parsedDate, "Invalid day should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_InvalidHour()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-10-31T38:29:11");
            Assert.IsNull(parsedDate, "Invalid hour should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_InvalidMinute()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-10-30T08:99:11");
            Assert.IsNull(parsedDate, "Invalid minute should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_InvalidSecond()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-10-01T08:29:60");
            Assert.IsNull(parsedDate, "Invalid second should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_InvalidSeparator()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-10-29_08:29:11");
            Assert.IsNull(parsedDate, "Invalid date-time separator should return a null date");
        }

        [TestMethod]
        public void TestParseEndDateString_YearOnly()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 12, 31, 23, 59, 59, 0, TimeSpan.Zero), parsedDate, "Year-only date should only contain year");
        }

        [TestMethod]
        public void TestParseEndDateString_YearAndMonthOnly()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-03");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 3, 31, 23, 59, 59, 0, TimeSpan.Zero), parsedDate, "Year and month date should only contain year and month");
        }

        [TestMethod]
        public void TestParseEndDateString_YearAndMonthOnly_LeapYear()
        {
            var parsedDate = DateHelper.ParseEndDateString("2012-02");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2012, 2, 29, 23, 59, 59, 0, TimeSpan.Zero), parsedDate, "Year and month date should only contain year and month");
        }

        [TestMethod]
        public void TestParseEndDateString_DateOnly()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-03-31");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 3, 31, 23, 59, 59, 0, TimeSpan.Zero), parsedDate, "Date should only contain year, month, and day");
        }

        [TestMethod]
        public void TestParseEndDateString_DateTime()
        {
            var parsedDate = DateHelper.ParseEndDateString("2019-03-31T08:29:11");
            Assert.IsNotNull(parsedDate, "Valid date should not return null");
            Assert.AreEqual(new DateTimeOffset(2019, 3, 31, 8, 29, 11, 0, TimeSpan.Zero), parsedDate, "Full Date Time not in the resulting date time");
        }
    }
}
