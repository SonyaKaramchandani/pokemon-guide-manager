using System;

namespace Biod.Insights.Service.Helpers
{
    public static class StringFormattingHelper
    {
        private const double TOLERANCE = double.Epsilon;
        
        public static string FormatTime(long seconds)
        {
            if (seconds < 60)
            {
                return seconds == 1 ? $"{seconds:0} second" : $"{seconds:0} seconds";
            }

            if (seconds < 3600)
            {
                var minutes = Math.Round(seconds / 60.0, 1, MidpointRounding.AwayFromZero);
                return Math.Abs(minutes - 1.0) < TOLERANCE ? $"{minutes:0.#} minute" : $"{minutes:0.#} minutes";
            }

            if (seconds < 86400)
            {
                var hours = Math.Round(seconds / 3600.0, 1, MidpointRounding.AwayFromZero);
                return Math.Abs(hours - 1.0) < TOLERANCE ? $"{hours:0.#} hour" : $"{hours:0.#} hours";
            }

            if (seconds < 31536000)
            {
                var days = Math.Round(seconds / 86400.0, 1, MidpointRounding.AwayFromZero);
                return Math.Abs(days - 1.0) < TOLERANCE ? $"{days:0.#} day" : $"{days:0.#} days";
            }

            var years = Math.Round(seconds / 31536000.0, 1, MidpointRounding.AwayFromZero);
            return Math.Abs(years - 1.0) < TOLERANCE ? $"{years:0.#} year" : $"{years:0.#} years";
        }

        public static string FormatPeriod(long? minSeconds, long? maxSeconds, long? avgSeconds)
        {
            if (minSeconds == null
                || maxSeconds == null
                || minSeconds == 0L && maxSeconds == 0
                || minSeconds > maxSeconds)
            {
                return avgSeconds.HasValue
                    ? $"{FormatTime(avgSeconds.Value)} average"
                    : null;
            }

            return $"{FormatTime(minSeconds.Value)} to {FormatTime(maxSeconds.Value)}" + (avgSeconds.HasValue
                ? $" ({FormatTime(avgSeconds.Value)} avg.)"
                : "");
        }

        /// <summary>
        /// Creates the date string with the year only if the year is not the current year
        /// </summary>
        /// <returns>the formatted date string</returns>
        public static string FormatDateWithConditionalYear(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "";
            }

            var currentYear = DateTime.Now.Year;
            var dateFormat = currentYear == dateTime.Value.Year ? "MMMM d" : "MMMM d, yyyy";
            return ((DateTime) dateTime).ToString(dateFormat);
        }
    }
}