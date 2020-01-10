using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Helpers
{
    public static class ArticleHelper
    {
        public static string GetDisplayName(ProcessedArticle article)
        {
            switch (article.ArticleFeedId)
            {
                case 3 when article.OriginalSourceUrl.Contains("cdc.gov"):
                case 9 when article.OriginalSourceUrl.Contains("wwwnc.cdc.gov"):
                    return "CDC";
                case 9 when article.OriginalSourceUrl.Contains("ecdc.europa.eu"):
                    return "ECDC";
                case 9 when article.OriginalSourceUrl.Contains("chp.gov.hk"):
                    return "Other Official";
                case 3:
                case 9:
                    return "News Media";
                default:
                    return article.ArticleFeed.DisplayName ?? "";
            }
        }

        public static int GetSeqId(ProcessedArticle article)
        {
            switch (article.ArticleFeedId)
            {
                case 3 when article.OriginalSourceUrl.Contains("cdc.gov"):
                case 9 when article.OriginalSourceUrl.Contains("wwwnc.cdc.gov"):
                    return 2;
                case 9:
                    return 6;
                case 3:
                    return 7;
                default:
                    return article.ArticleFeed.SeqId ?? 0;
            }
        }
    }
}