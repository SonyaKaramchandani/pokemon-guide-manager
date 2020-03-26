namespace Biod.Insights.Common
{
    public static class StringExtensions
    {
        public static string DefaultIfWhiteSpace(this string value, string newValue = null)
        {
            return string.IsNullOrWhiteSpace(value) ? newValue : value;
        }
    }
}