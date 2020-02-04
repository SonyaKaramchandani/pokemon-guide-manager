namespace Biod.Insights.Api.Helpers
{
    public static class StringFormattingHelper
    {
        public static string FormatDuration(long seconds)
        {
            if (seconds < 3600)
            {
                var minutes = seconds / 60.0;
                return minutes == 1 ? $"{minutes:0.#} minute" : $"{minutes:0.#} minutes";
            }
            if (seconds < 86400)
            {
                var hours = seconds / 3600.0;
                return hours == 1 ? $"{hours:0.#} hour" : $"{hours:0.#} hours";
            }
            if (seconds < 31536000)
            {
                var days = seconds / 86400.0;
                return days == 1 ? $"{days:0.#} day" : $"{days:0.#} days";
            }
            
            var years = seconds / 31536000.0;
            return years == 1 ? $"{years:0.#} year" : $"{years:0.#} years";
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