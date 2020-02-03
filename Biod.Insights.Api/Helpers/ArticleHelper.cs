using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Helpers
{
    public static class ArticleHelper
    {
        public static string GetDisplayName(int? feedId, string sourceUrl, string feedDisplayName)
        {
            switch (feedId)
            {
                case 3 when sourceUrl.Contains("cdc.gov"):
                case 9 when sourceUrl.Contains("wwwnc.cdc.gov"):
                    return "CDC";
                case 9 when sourceUrl.Contains("ecdc.europa.eu"):
                    return "ECDC";
                case 9 when sourceUrl.Contains("chp.gov.hk"):
                    return "Other Official";
                case 3:
                case 9:
                    return "News Media";
                default:
                    return feedDisplayName ?? "";
            }
        }

        public static int GetSeqId(int? feedId, string sourceUrl, int? seqId)
        {
            switch (feedId)
            {
                case 3 when sourceUrl.Contains("cdc.gov"):
                case 9 when sourceUrl.Contains("wwwnc.cdc.gov"):
                    return 2;
                case 9:
                    return 6;
                case 3:
                    return 7;
                default:
                    return seqId ?? 0;
            }
        }
    }
}