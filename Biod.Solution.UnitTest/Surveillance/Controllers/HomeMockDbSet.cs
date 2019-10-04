using Biod.Surveillance.Models.Surveillance;
using Moq;
using System.Collections.Generic;

namespace Biod.Solution.UnitTest.Controllers.Surveillance
{
    class HomeMockDbSet
    {
        public static readonly decimal CLUSTER_ID = 10222462;
        public static readonly int ARTICLE_FEED1_ID = 15622927;
        public static readonly string ARTICLE_FEED1_NAME = "ProMED";
        public static readonly int ARTICLE_FEED2_ID = 78816031;
        public static readonly string ARTICLE_FEED2_NAME = "WHO";
        public static readonly int ARTICLE_ID_NO_CLUSTER = 58638955;
        public static readonly int ARTICLE_ID_CLUSTER_PARENT = 26551937;
        public static readonly int ARTICLE_ID_CLUSTER_CHILD = 57584878;

        public static readonly int HAM_DISEASE_ACTIVITY = 3;

        public static readonly ProcessedArticle ARTICLE1 = new ProcessedArticle()
        {
            ArticleId = ARTICLE_ID_NO_CLUSTER.ToString(),
            ArticleTitle = "Meningitis - Sudan: Darfur region",
            SystemLastModifiedDate = new System.DateTime(),
            ArticleFeedId = ARTICLE_FEED1_ID,
            FeedURL = "http://www.promedmail.org/post/20180402.5724160",
            FeedSourceId = "20180402.5724160",
            FeedPublishedDate = new System.DateTime(),
            HamTypeId = HAM_DISEASE_ACTIVITY,
            OriginalSourceURL = "http://allafrica.com/stories/201804020088.html",
            OriginalLanguage = "en",
            ArticleBody = "Tests have confirmed 14 cases of meningitis in Central Darfur, with reports of other cases in South, North, and East Darfur.",
            IsRead = false,
            IsImportant = false
        };

        public static readonly ProcessedArticle ARTICLE2 = new ProcessedArticle()
        {
            ArticleId = ARTICLE_ID_CLUSTER_PARENT.ToString(),
            ArticleTitle = "WHO to build Ebola labs in Sierra Leone",
            SystemLastModifiedDate = new System.DateTime(),
            ArticleFeedId = ARTICLE_FEED2_ID,
            FeedSourceId = "20181004014500-1790",
            FeedPublishedDate = new System.DateTime(),
            HamTypeId = HAM_DISEASE_ACTIVITY,
            OriginalSourceURL = "http://politicosl.com/articles/who-build-ebola-labs-sierra-leone",
            SimilarClusterId = CLUSTER_ID,
            OriginalLanguage = "en",
            ArticleBody = "WHO’s Country Representative in Sierra Leone, Dr Jacob Mufundu, has told journalists in Freetown that his organisation will soon set up Ebola testing mobile laboratories in Kenema, Makeni, Freetown and other parts of the country.",
            IsRead = false,
            IsImportant = false
        };

        public static readonly ProcessedArticle ARTICLE3 = new ProcessedArticle()
        {
            ArticleId = ARTICLE_ID_CLUSTER_CHILD.ToString(),
            ArticleTitle = "Sierra Leone News: How village is recovering from the Ebola crisis",
            SystemLastModifiedDate = new System.DateTime(),
            ArticleFeedId = ARTICLE_FEED2_ID,
            FeedSourceId = "20181003181500-717",
            FeedPublishedDate = new System.DateTime(),
            HamTypeId = HAM_DISEASE_ACTIVITY,
            OriginalSourceURL = "https://awoko.org/2018/10/03/sierra-leone-news-how-village-is-recovering-from-the-ebola-crisis/",
            SimilarClusterId = CLUSTER_ID,
            OriginalLanguage = "en",
            ArticleBody = "People from a remote village in Sierra Leone have documented the rebuilding of their community from the Ebola epidemic – by taking photographs of themselves getting on with daily life.",
            IsRead = false,
            IsImportant = false
        };

        public Mock<BiodSurveillanceDataEntities> MockContext { get; set; }

        public HomeMockDbSet()
        {
            var mockedArticles = CreateMockedArticles();

            // Initialize a Mock DB with test data
            MockContext = new Mock<BiodSurveillanceDataEntities>();
            MockContext.Setup(context => context.ArticleFeeds).ReturnsDbSet(mockedArticles);
            MockContext.Setup(context => context.ProcessedArticles).ReturnsDbSet(new List<ProcessedArticle> { ARTICLE1, ARTICLE2, ARTICLE3 });
        }

        public static List<ArticleFeed> CreateMockedArticles()
        {
            return new List<ArticleFeed>
            {
                new ArticleFeed()
                {
                    ArticleFeedId = ARTICLE_FEED1_ID,
                    ArticleFeedName = ARTICLE_FEED1_NAME,
                    ProcessedArticles = new List<ProcessedArticle> { ARTICLE1 }
                },
                new ArticleFeed()
                {
                    ArticleFeedId = ARTICLE_FEED2_ID,
                    ArticleFeedName = ARTICLE_FEED2_NAME,
                    ProcessedArticles = new List<ProcessedArticle> { ARTICLE2, ARTICLE3 }
                }
            };
        }
    }
}
