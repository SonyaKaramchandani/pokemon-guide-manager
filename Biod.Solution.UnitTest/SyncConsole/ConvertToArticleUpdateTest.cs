using Biod.Surveillance.Zebra.SyncConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biod.Zebra.Library.Models.Surveillance;
using System;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.EntityModels.Surveillance;

namespace Biod.Solution.UnitTest.SyncConsole
{
    /// <summary>
    /// Tests the ConvertToArticleUpdate method in the SyncConsole program
    /// </summary>
    [TestClass]
    public class ConvertToArticleUpdateTest
    {
        private readonly Random random = new Random();

        /// <summary>
        /// Tests whether the exception is thrown when null is passed for the article model
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "An article of null was inappropriately allowed")]
        public void NullObject()
        {
            Program.ConvertToArticleUpdate(null);
        }

        /// <summary>
        /// Tests whether all fields are correctly converted to the output model
        /// </summary>
        [TestMethod]
        public void AllFields()
        {
            string articleId = random.Next(1, 10000).ToString();
            string articleTitle = random.Next(1, 10000).ToString();
            DateTime systemLastModifiedDate = DateTime.Now.AddMinutes(random.Next(1, 10000));
            decimal certaintyScore = (decimal)random.NextDouble();
            int articleFeedId = random.Next(1, 10000);
            string feedUrl = random.Next(1, 10000).ToString();
            string feedSourceId = random.Next(1, 10000).ToString();
            DateTime feedPublishedDate = DateTime.Now.AddMinutes(random.Next(1, 10000));
            int hamTypeId = random.Next(1, 10000);
            string originalSourceURL = random.Next(1, 10000).ToString();
            bool isCompleted = random.Next(1, 10000) % 2 == 0;
            decimal similarClusterId = (decimal)random.NextDouble();
            string originalLanguage = random.Next(1, 10000).ToString();
            DateTime userLastModifiedDate = DateTime.Now.AddMinutes(random.Next(1, 10000));
            string lastUpdatedByUserName = random.Next(1, 10000).ToString();
            string notes = random.Next(1, 10000).ToString();
            string articleBody = random.Next(1, 10000).ToString();
            bool isRead = random.Next(1, 10000) % 2 == 0;

            SurveillanceProcessedArticle article = new SurveillanceProcessedArticle()
            {
                ArticleId = articleId,
                ArticleTitle = articleTitle,
                SystemLastModifiedDate = systemLastModifiedDate,
                CertaintyScore = certaintyScore,
                ArticleFeedId = articleFeedId,
                FeedURL = feedUrl,
                FeedSourceId = feedSourceId,
                FeedPublishedDate = feedPublishedDate,
                HamTypeId = hamTypeId,
                OriginalSourceURL = originalSourceURL,
                IsCompleted = isCompleted,
                SimilarClusterId = similarClusterId,
                OriginalLanguage = originalLanguage,
                UserLastModifiedDate = userLastModifiedDate,
                LastUpdatedByUserName = lastUpdatedByUserName,
                Notes = notes,
                ArticleBody = articleBody,
                IsRead = isRead
            };

            ArticleUpdateForZebra result = Program.ConvertToArticleUpdate(article);

            Assert.AreEqual(result.ArticleId, articleId, "ArticleId not mapped correctly");
            Assert.AreEqual(result.ArticleTitle, articleTitle, "ArticleTitle not mapped correctly");
            Assert.AreEqual(result.SystemLastModifiedDate, systemLastModifiedDate, "SystemLastModifiedDate not mapped correctly");
            Assert.AreEqual(result.CertaintyScore, certaintyScore, "CertaintyScore not mapped correctly");
            Assert.AreEqual(result.ArticleFeedId, articleFeedId, "ArticleFeedId not mapped correctly");
            Assert.AreEqual(result.FeedURL, feedUrl, "FeedURL not mapped correctly");
            Assert.AreEqual(result.FeedSourceId, feedSourceId, "FeedSourceId not mapped correctly");
            Assert.AreEqual(result.FeedPublishedDate, feedPublishedDate, "FeedPublishedDate not mapped correctly");
            Assert.AreEqual(result.HamTypeId, hamTypeId, "HamTypeId not mapped correctly");
            Assert.AreEqual(result.OriginalSourceURL, originalSourceURL, "OriginalSourceURL not mapped correctly");
            Assert.AreEqual(result.IsCompleted, isCompleted, "IsCompleted not mapped correctly");
            Assert.AreEqual(result.SimilarClusterId, similarClusterId, "SimilarClusterId not mapped correctly");
            Assert.AreEqual(result.OriginalLanguage, originalLanguage, "OriginalLanguage not mapped correctly");
            Assert.AreEqual(result.LastUpdatedByUserName, lastUpdatedByUserName, "LastUpdatedByUserName not mapped correctly");
            Assert.AreEqual(result.Notes, notes, "Notes not mapped correctly");
            Assert.AreEqual(result.ArticleBody, null, "ArticleBody not mapped correctly");
            Assert.AreEqual(result.IsRead, isRead, "IsRead not mapped correctly");

            Assert.AreEqual(result.DiseaseObject, "", "DiseaseObject field did not default to empty string");
            Assert.IsNotNull(result.SelectedPublishedEventIds, "SelectedPublishedEventIds should not be null");
            Assert.AreEqual(result.SelectedPublishedEventIds.Count, 0, "SelectedPublishedEventIds should be defaulted as empty");
        }

        /// <summary>
        /// Tests whether the fields with null are not being mutated in the converted model
        /// </summary>
        [TestMethod]
        public void EmptyFields()
        {
            SurveillanceProcessedArticle article = new SurveillanceProcessedArticle()
            {
                ArticleId = null,
                ArticleTitle = null,
                CertaintyScore = null,
                ArticleFeedId = null,
                FeedURL = null,
                FeedSourceId = null,
                HamTypeId = null,
                OriginalSourceURL = null,
                IsCompleted = null,
                SimilarClusterId = null,
                OriginalLanguage = null,
                UserLastModifiedDate = null,
                LastUpdatedByUserName = null,
                Notes = null,
                ArticleBody = null,
                IsRead = null
            };

            ArticleUpdateForZebra result = Program.ConvertToArticleUpdate(article);

            Assert.AreEqual(result.ArticleId, null, "ArticleId not mapped correctly");
            Assert.AreEqual(result.ArticleTitle, null, "ArticleTitle not mapped correctly");
            Assert.AreEqual(result.CertaintyScore, null, "CertaintyScore not mapped correctly");
            Assert.AreEqual(result.ArticleFeedId, null, "ArticleFeedId not mapped correctly");
            Assert.AreEqual(result.FeedURL, null, "FeedURL not mapped correctly");
            Assert.AreEqual(result.FeedSourceId, null, "FeedSourceId not mapped correctly");
            Assert.AreEqual(result.HamTypeId, null, "HamTypeId not mapped correctly");
            Assert.AreEqual(result.OriginalSourceURL, null, "OriginalSourceURL not mapped correctly");
            Assert.AreEqual(result.IsCompleted, null, "IsCompleted not mapped correctly");
            Assert.AreEqual(result.SimilarClusterId, null, "SimilarClusterId not mapped correctly");
            Assert.AreEqual(result.OriginalLanguage, null, "OriginalLanguage not mapped correctly");
            Assert.AreEqual(result.LastUpdatedByUserName, null, "LastUpdatedByUserName not mapped correctly");
            Assert.AreEqual(result.Notes, null, "Notes not mapped correctly");
            Assert.AreEqual(result.ArticleBody, null, "ArticleBody not mapped correctly");
            Assert.AreEqual(result.IsRead, null, "IsRead not mapped correctly");

            Assert.AreEqual(result.DiseaseObject, "", "DiseaseObject field did not default to empty string");
            Assert.IsNotNull(result.SelectedPublishedEventIds, "SelectedPublishedEventIds should not be null");
            Assert.AreEqual(result.SelectedPublishedEventIds.Count, 0, "SelectedPublishedEventIds should be defaulted as empty");
        }
    }
}
