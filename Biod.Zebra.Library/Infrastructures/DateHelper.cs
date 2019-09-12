using System;
using System.Text.RegularExpressions;

namespace Biod.Zebra.Library.Infrastructures
{
    public static class DateHelper
    {
        public static string DATE_TIME_OFFSET_REGEX = @"^(\d{4})(?:-(1[0-2]|0[1-9])(?:-(0[1-9]|[12]\d|3[01])(?:[T ]([01]\d|2[0-3]):([0-5]\d):([0-5]\d))?)?)?$";

        /// <summary>
        /// Parses the date string into a DateTimeOffset, filling with minimum values for missing fields.
        /// e.g. 2019 becomes 2019-01-01T00:00:00
        /// 
        /// Note: All times are in UTC
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTimeOffset? ParseStartDateString(string dateString)
        {
            var match = Regex.Match(dateString ?? "", DATE_TIME_OFFSET_REGEX);
            if (!match.Success)
            {
                return null;
            }

            var year = match.Groups[1];
            var month = match.Groups[2];
            var day = match.Groups[3];
            // The following fields are all or none
            var hour = match.Groups[4].Value.TrimStart('0');
            var second = match.Groups[6].Value.TrimStart('0');
            var minute = match.Groups[5].Value.TrimStart('0');

            return new DateTimeOffset(
                Convert.ToInt16(year.Value.TrimStart('0')),
                string.IsNullOrEmpty(month.Value) ? 1 : Convert.ToInt16(month.Value.TrimStart('0')),
                string.IsNullOrEmpty(day.Value) ? 1 : Convert.ToInt16(day.Value.TrimStart('0')),
                string.IsNullOrEmpty(hour) ? 0 : Convert.ToInt16(hour),
                string.IsNullOrEmpty(minute) ? 0 : Convert.ToInt16(minute),
                string.IsNullOrEmpty(second) ? 0 : Convert.ToInt16(second),
                TimeSpan.Zero);
        }

        /// <summary>
        /// Parses the date string into a DateTimeOffset, filling with maximum values for missing fields.
        /// e.g. 2019 becomes 2019-12-31T23:59:59
        /// 
        /// Note: All times are in UTC
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTimeOffset? ParseEndDateString(string dateString)
        {
            var match = Regex.Match(dateString ?? "", DATE_TIME_OFFSET_REGEX);
            if (!match.Success)
            {
                return null;
            }

            var year = Convert.ToInt16(match.Groups[1].Value.TrimStart('0'));
            var month = string.IsNullOrEmpty(match.Groups[2].Value) ? 12 : Convert.ToInt16(match.Groups[2].Value.TrimStart('0'));
            var day = match.Groups[3];
            // The following fields are all or none
            var hour = match.Groups[4].Value;
            hour = (string.IsNullOrEmpty(hour) ? "23" : hour).Trim('0');
            var minute = match.Groups[5].Value;
            minute = (string.IsNullOrEmpty(minute) ? "59" : minute).Trim('0');
            var second = match.Groups[6].Value;
            second = (string.IsNullOrEmpty(second) ? "59" : second).Trim('0');

            return new DateTimeOffset(
                year,
                month,
                string.IsNullOrEmpty(day.Value) ? DateTime.DaysInMonth(year, month) : Convert.ToInt16(day.Value.TrimStart('0')),
                string.IsNullOrEmpty(hour) ? 0 : Convert.ToInt16(hour),
                string.IsNullOrEmpty(minute) ? 0 : Convert.ToInt16(minute),
                string.IsNullOrEmpty(second) ? 0 : Convert.ToInt16(second),
                TimeSpan.Zero);
        }
    }
}
