using System;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest.Infrastructures
{
    [TestClass]
    public class StringFormattingHelperTest
    {
        #region FormatInteger tests

        [TestMethod]
        public void TestFormatInteger_LargeNumber()
        {
            var formattedString = StringFormattingHelper.FormatInteger(1234567890);
            Assert.AreEqual("1,234,567,890", formattedString, "Large number not formatted properly with comma separators");
        }

        [TestMethod]
        public void TestFormatInteger_SingleDigit_LowerBound()
        {
            var formattedString = StringFormattingHelper.FormatInteger(1);
            Assert.AreEqual("One", formattedString, "Single digit number not formatted as text form");
        }

        [TestMethod]
        public void TestFormatInteger_SingleDigit_UpperBound()
        {
            var formattedString = StringFormattingHelper.FormatInteger(9);
            Assert.AreEqual("Nine", formattedString, "Single digit number not formatted as text form");
        }

        [TestMethod]
        public void TestFormatInteger_Zero()
        {
            var formattedString = StringFormattingHelper.FormatInteger(0);
            Assert.AreEqual("0", formattedString, "Zero not formatted as expected");
        }

        [TestMethod]
        public void TestFormatInteger_Negative()
        {
            var formattedString = StringFormattingHelper.FormatInteger(-283723);
            Assert.AreEqual("-283723", formattedString, "Negative number not formatted as expected");
        }

        #endregion

        #region FormatAverageProbability tests

        /// <summary>
        /// Tests the behaviour when the range is negative
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_NegativeRange()
        {
            var formattedString = StringFormattingHelper.FormatAverageProbability((decimal)-0.5, 0, out decimal avgVal);
            Assert.AreEqual(0, avgVal, "Negative range should be 0");
            Assert.AreEqual("0%", formattedString, "Negative range not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the max is less than the min value.
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_InvalidRange()
        {
            var formattedString = StringFormattingHelper.FormatAverageProbability(1, 0, out decimal avgVal);
            Assert.AreEqual(0, avgVal, "Invalid range where max is smaller than min should be 0");
            Assert.AreEqual("0%", formattedString, "Invalid range where max is smaller than min not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the max is equal to the min value
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_EqualRange()
        {
            var formattedString = StringFormattingHelper.FormatAverageProbability((decimal)0.341, (decimal)0.341, out decimal avgVal);
            Assert.AreEqual((decimal)34.1, avgVal, "Average should be the same as min/max if the min and max are equivalent");
            Assert.AreEqual("34%", formattedString, "Min equivalent to max not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the average value is a whole number that does not need rounding
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_WholeNumberAverage()
        {
            var formattedString = StringFormattingHelper.FormatAverageProbability((decimal)0.4, (decimal)0.6, out decimal avgVal);
            Assert.AreEqual(50, avgVal, "Whole number average not being computed as expected");
            Assert.AreEqual("50%", formattedString, "Whole number average not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the average value is a rounded down
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_RoundedDownAverage()
        {
            var formattedString = StringFormattingHelper.FormatAverageProbability((decimal)0.2387, (decimal)0.4679, out decimal avgVal);
            Assert.AreEqual((decimal)35.33, avgVal, "Rounded down average not being computed as expected");
            Assert.AreEqual("35%", formattedString, "Rounded dow average not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the average value is a rounded up
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_RoundedUpAverage()
        {
            var formattedString = StringFormattingHelper.FormatAverageProbability((decimal)0.4586, (decimal)0.4761, out decimal avgVal);
            Assert.AreEqual((decimal)46.735, avgVal, "Rounded up average not being computed as expected");
            Assert.AreEqual("47%", formattedString, "Rounded up average not formatted as expected");
        }

        /// <summary>
        /// Tests the string formatted when the average is larger than 90
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_GreaterThan90()
        {
            var formattedString = StringFormattingHelper.FormatAverageProbability((decimal)0.9, 1, out _);
            Assert.AreEqual(">90%", formattedString, "Average larger than 90% not formatted as expected");
        }

        #endregion

        #region FormatAverage tests

        /// <summary>
        /// Tests the behaviour when the range is negative
        /// </summary>
        [TestMethod]
        public void TestFormatAverage_NegativeRange()
        {
            var formattedString = StringFormattingHelper.FormatAverage(-128, 0, out decimal avgVal);
            Assert.AreEqual(0, avgVal, "Negative range should be 0");
            Assert.AreEqual("<1", formattedString, "Negative range not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the max is less than the min value.
        /// </summary>
        [TestMethod]
        public void TestFormatAverage_InvalidRange()
        {
            var formattedString = StringFormattingHelper.FormatAverage(2321, 2320, out decimal avgVal);
            Assert.AreEqual(0, avgVal, "Invalid range where max is smaller than min should be 0");
            Assert.AreEqual("<1", formattedString, "Invalid range where max is smaller than min not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the max is equal to the min value
        /// </summary>
        [TestMethod]
        public void TestFormatAverage_EqualRange()
        {
            var formattedString = StringFormattingHelper.FormatAverage((decimal)482.28, (decimal)482.28, out decimal avgVal);
            Assert.AreEqual((decimal)482.28, avgVal, "Average should be the same as min/max if the min and max are equivalent");
            Assert.AreEqual("482", formattedString, "Min equivalent to max not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the average value is a whole number that does not need rounding
        /// </summary>
        [TestMethod]
        public void TestFormatAverage_WholeNumberAverage()
        {
            var formattedString = StringFormattingHelper.FormatAverage((decimal)10.5, (decimal)89.5, out decimal avgVal);
            Assert.AreEqual(50, avgVal, "Whole number average not being computed as expected");
            Assert.AreEqual("50", formattedString, "Whole number average not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the average value is a rounded down
        /// </summary>
        [TestMethod]
        public void TestFormatAverage_RoundedDownAverage()
        {
            var formattedString = StringFormattingHelper.FormatAverage((decimal)24.57, (decimal)47.77, out decimal avgVal);
            Assert.AreEqual((decimal)36.17, avgVal, "Rounded down average not being computed as expected");
            Assert.AreEqual("36", formattedString, "Rounded dow average not formatted as expected");
        }

        /// <summary>
        /// Tests the behaviour when the average value is a rounded up
        /// </summary>
        [TestMethod]
        public void TestFormatAverage_RoundedUpAverage()
        {
            var formattedString = StringFormattingHelper.FormatAverage((decimal)45.76, (decimal)83.48, out decimal avgVal);
            Assert.AreEqual((decimal)64.62, avgVal, "Rounded up average not being computed as expected");
            Assert.AreEqual("65", formattedString, "Rounded up average not formatted as expected");
        }

        /// <summary>
        /// Tests the string formatted when the average is less than 1
        /// </summary>
        [TestMethod]
        public void TestFormatAverageProbability_LessThan1()
        {
            var formattedString = StringFormattingHelper.FormatAverage((decimal)0.3, 1, out _);
            Assert.AreEqual("<1", formattedString, "Average smaller than 1 not formatted as expected");
        }

        #endregion

    }
}
