namespace Biod.Insights.Api.Helpers
{
    public static class StringFormattingHelper
    {
        public static string FormatDuration(long seconds)
        {
            if (seconds < 3600)
            {
                var minutes = seconds / 60;
                return minutes == 1 ? $"{minutes} minute" : $"{minutes} minutes";
            }
            if (seconds < 86400)
            {
                var hours = seconds / 3600;
                return hours == 1 ? $"{hours} hour" : $"{hours} hours";
            }

            var days = seconds / 86400;
            return days == 1 ? $"{days} day" : $"{days} days";
        }
        
        public static string FormatIncubationPeriod(long? minSeconds, long? maxSeconds, long? avgSeconds)
        {
            if (minSeconds == null || maxSeconds == null || avgSeconds == null)
            {
                return "-";
            }
            return $"{FormatDuration(minSeconds.Value)} to {FormatDuration(maxSeconds.Value)} ({FormatDuration(avgSeconds.Value)} avg.)";
        }
    }
}