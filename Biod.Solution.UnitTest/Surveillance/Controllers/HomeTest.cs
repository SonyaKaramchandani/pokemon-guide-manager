using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biod.Surveillance.Controllers;
using Biod.Surveillance.Models.Surveillance;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Biod.Surveillance.ViewModels;

namespace Biod.Solution.UnitTest.Controllers.Surveillance
{
    [TestClass]
    public class HomeTest
    {
        private HomeController controller;
        private Mock<BiodSurveillanceDataEntities> mockDbContext;
        private HomeMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new HomeMockDbSet();
            mockDbContext = dbMock.MockContext;

            controller = new HomeController(mockDbContext.Object);
        }

        /// <summary>
        /// Checks that no exception is thrown and an empty list is returned 
        /// when a null input is provided when obtaining parent articles
        /// </summary>
        [TestMethod]
        public void GetParentArticles_Null()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new List<ArticleGridWithSimilarCluster>(), controller.GetParentArticles(null)));
        }

        /// <summary>
        /// Checks that no exception is thrown and an empty list is returned 
        /// when no input is provided when obtaining parent articles
        /// </summary>
        [TestMethod]
        public void GetParentArticles_Empty()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new List<ArticleGridWithSimilarCluster>(), controller.GetParentArticles(new List<IGrouping<decimal?, ProcessedArticle>>())));
        }

        /// <summary>
        /// Checks that child articles are filtered out from the list
        /// when obtaining parent articles
        /// </summary>
        [TestMethod]
        public void GetParentArticles_NonEmpty()
        {
            var articles = mockDbContext.Object.ProcessedArticles.GroupBy(a => a.SimilarClusterId).ToList();
            var result = controller.GetParentArticles(articles);
            var expected = new List<ArticleGridWithSimilarCluster> {
                new ArticleGridWithSimilarCluster(HomeMockDbSet.ARTICLE1)
                {
                    ArticleFeedName = HomeMockDbSet.ARTICLE_FEED1_NAME,
                    HasChildArticle = false
                },
                new ArticleGridWithSimilarCluster(HomeMockDbSet.ARTICLE2)
                {
                    ArticleFeedName = HomeMockDbSet.ARTICLE_FEED2_NAME,
                    HasChildArticle = true
                }
            };

            Assert.IsTrue(Enumerable.SequenceEqual(expected, result));
        }
    }
}