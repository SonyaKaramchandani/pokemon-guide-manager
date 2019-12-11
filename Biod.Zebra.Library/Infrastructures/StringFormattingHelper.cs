using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Biod.Zebra.Library.Infrastructures
{
    public static class StringFormattingHelper
    {
        public static readonly Dictionary<int, string> SINGLE_DIGIT_NUMBERS = new Dictionary<int, string>()
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
            { 4, "Four" },
            { 5, "Five" },
            { 6, "Six" },
            { 7, "Seven" },
            { 8, "Eight" },
            { 9, "Nine" }
        };

        public static readonly Dictionary<string, string> PLURAL_TABLE = new Dictionary<string, string>
        {
            {"case", "cases"},
            {"disease", "diseases"},
            {"symptom", "symptoms"},
            {"system", "systems"},
            {"vacine", "vacines"},
            {"article", "articles"},
            {"event", "events"},
            {"number", "numbers"},
            {"location", "locations"}
        };

        /// <summary>
        /// Formats the DateTime object to the format of 'MMM. d, yyyy' where the month will have the
        /// period only if the month name has been abbreviated
        /// </summary>
        /// <param name="date">The date object to format</param>
        /// <returns>The formatted date</returns>
        public static string FormatShortDate(DateTime date)
        {
            if (date.ToString("MMM").Equals(date.ToString("MMMM")))
            {
                return date.ToString("MMM d, yyyy");
            }
            else
            {
                return date.ToString("MMM. d, yyyy");
            }
        }

        /// <summary>
        /// Creates the date string with the year only if the year is not the current year
        /// </summary>
        /// <returns>the formatted date string</returns>
        public static string FormatDateWithConditionalYear(DateTime? dateTime)
        {
            var currentYear = DateTime.Now.Year;
            var dateFormat = currentYear == dateTime?.Year ? "MMMM d" : "MMMM d, yyyy";
            return dateTime == null ? "" : ((DateTime)dateTime).ToString(dateFormat);
        }

        /// <summary>
        /// Formats the min to max values to an interval display text
        /// </summary>
        /// <param name="minVal">the min value</param>
        /// <param name="maxVal">the max value</param>
        /// <param name="unit">the format of the interval (e.g. "%")</param>
        /// <returns></returns>
        public static string GetInterval(decimal minVal, decimal maxVal, string unit = "")
        {
            string retVal;
            var prefixLow = "";
            var prefixUp = "";

            if (unit == "%")
            {
                minVal *= 100;
                maxVal *= 100;
                if (minVal > 90)
                {
                    minVal = 90;
                    prefixLow = ">";
                }
                if (maxVal > 90)
                {
                    maxVal = 90;
                    prefixUp = ">";
                }
            }

            prefixLow = prefixLow.Length > 0 ? prefixLow : minVal < 1 ? "<" : "";
            var roundMin = minVal >= 1 ? Math.Round(minVal, 0) : 1;
            var roundMax = maxVal >= 1 ? Math.Round(maxVal, 0) : 1;

            if (roundMin == roundMax && prefixLow != "<")
            {
                prefixLow = prefixLow.Length > 0 ? prefixLow : "~";
                retVal = prefixLow + roundMin + unit;
            }
            else
            {
                retVal = prefixLow + roundMin + unit + " to " + prefixUp + roundMax + unit;
            }

            return retVal;
        }

        /// <summary>
        /// Gets the display text for the range of travellers given the min and max travellers.
        /// 
        /// Precondition: maxVal >= minVal
        /// </summary>
        /// <param name="minVal">the minimum volume of travellers</param>
        /// <param name="maxVal">the maximum volume of travellers</param>
        /// <param name="includeUnit">whether to include the word "Traveller" as a unit</param>
        /// <returns>the formatted string representing the interval</returns>
        /// <exception cref="System.ArgumentException">Thrown when the precondition fails</exception>
        public static string GetTravellerInterval(decimal minVal, decimal maxVal, bool includeUnit = false)
        {
            if (minVal > maxVal)
            {
                throw new ArgumentException($"minVal {minVal} should not be greater than maxVal {maxVal}");
            }

            if (maxVal <= 0)
            {
                return "Negligible";
            }

            // Calculated rounded values
            var roundedMin = Math.Round(minVal, 0);
            var roundedMax = Math.Round(maxVal, 0);
            
            var unit = "";
            if (includeUnit)
            {
                unit = " Traveller" + (roundedMax > 1 ? "s" : "");
            }

            if (minVal < 1)
            {
                if (maxVal < 1)
                {
                    return $"< 1{unit}";
                }
                return $"< 1 to {roundedMax}{unit}";
            }

            if (minVal == maxVal || roundedMin == roundedMax)
            {
                return $"~ {roundedMin}{unit}";
            }
            
            return $"{roundedMin} to {roundedMax}{unit}";
        }

        /// <summary>
        /// Formats the string for the average probability. Defaults to 0 if invalid min/max values
        /// </summary>
        /// <param name="minVal">the min probability between 0 and 1</param>
        /// <param name="maxVal">the max probability between 0 and 1</param>
        /// <param name="avgVal">the calculated average probability</param>
        /// <returns>the formatted string of the average probability</returns>
        public static string FormatAverageProbability(decimal minVal, decimal maxVal, out decimal avgVal)
        {
            minVal *= 100;
            maxVal *= 100;
            avgVal = 0;

            if (minVal >= 0 && minVal <= maxVal)
            {
                avgVal = (minVal + maxVal) / 2;
            }

            return $"{(avgVal > 90 ? ">90" : Math.Round(avgVal, 0).ToString(CultureInfo.InvariantCulture))}%";
        }

        /// <summary>
        /// Formats the string for the average value. Defaults to 0 if invalid min/max values
        /// </summary>
        /// <param name="minVal">the min value >= 0</param>
        /// <param name="maxVal">the max value >= 0</param>
        /// <param name="avgVal">the average value</param>
        /// <returns></returns>
        public static string FormatAverage(decimal minVal, decimal maxVal, out decimal avgVal)
        {
            avgVal = 0;

            if (minVal >= 0 && minVal <= maxVal)
            {
                avgVal = (minVal + maxVal) / 2;
            }

            return $"{(avgVal < 1 ? "<1" : Math.Round(avgVal, 0).ToString(CultureInfo.InvariantCulture))}";
        }

        /// <summary>
        /// Formats the integer to be a string, where the numbers 1-9 are displayed as english one to nine.
        /// All other numbers are formatted as a numeric string, with comma-separated thousands.
        /// </summary>
        /// <param name="number">the number to format</param>
        /// <returns>the formatted number string</returns>
        public static string FormatInteger(int number)
        {
            if (number >= 10)
            {
                return number.ToString("N0");
            }
            else if (number > 0)
            {
                return SINGLE_DIGIT_NUMBERS.First(n => n.Key == number).Value;
            }

            return number.ToString();
        }

        /// <summary>Formats the word as plural by number.</summary>
        /// <param name="word">The word.</param>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string FormatWordAsPluralByNumber(string word, int number)
        {
            // Special-case count of one.
            // ... Otherwise, return the pluralized word.
            if (number == 1)
            {
                return word;
            }
            return PLURAL_TABLE[word];
        }

        /// <summary>Formats the word as plural by word.</summary>
        /// <param name="word">The word.</param>
        /// <param name="numberAsWord">The number as word.</param>
        /// <returns></returns>
        public static string FormatWordAsPluralByWord(string word, string numberAsWord)
        {
            if (numberAsWord.ToLower() == "one")
            {
                return word;
            }
            //return the pluralized word.
            return PLURAL_TABLE[word];
        }

        /// <summary>Formats the word as plural.</summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string FormatWordAsPlural(string word)
        {
            //return the pluralized word.
            return PLURAL_TABLE[word];
        }

        /// <summary>Determines whether the specified string as number is numeric.</summary>
        /// <param name="stringAsNumber">The string as number.</param>
        /// <returns>
        ///   <c>true</c> if the specified string as number is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(string stringAsNumber)
        {
            return float.TryParse(stringAsNumber, out float output);
        }
    }
}
