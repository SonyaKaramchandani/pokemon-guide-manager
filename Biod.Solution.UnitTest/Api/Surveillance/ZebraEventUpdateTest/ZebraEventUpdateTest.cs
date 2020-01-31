using Biod.Zebra.Api.Surveillance;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Surveillance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Biod.Solution.UnitTest.Api.Surveillance
{
    /// <summary>
    /// Tests the API controller for the event update
    /// </summary>
    [TestClass]
    public class ZebraEventUpdateTest
    {
        private readonly Random random = new Random();

        private ZebraEventUpdateController controller;
        private Mock<BiodZebraEntities> mockDbContext;
        private ZebraEventUpdateMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraEventUpdateMockDbSet();
            mockDbContext = dbMock.MockContext;

            // Configure the controller request
            controller = new ZebraEventUpdateController
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.SetConfiguration(new HttpConfiguration());

            // Replace db context in controller
            controller.DbContext = mockDbContext.Object;

            // Replace helper service in controller
            controller.RequestResponseService = new MockZebraUpdateService();
        }

        /// <summary>
        /// Checks whether an invalid model state will return 400 Bad Request
        /// </summary>
        [TestMethod]
        public async Task InvalidModelState()
        {
            controller.ModelState.AddModelError("test", "test");

            HttpResponseMessage result = await controller.PostAsync(null);
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, result.StatusCode, "Invalid model state not returning 400 Bad Request");
        }

        /// <summary>
        /// Checks whether an uncaught exception will return 500 Internal Server Error
        /// </summary>
        [TestMethod]
        public async Task UncaughtExceptionError()
        {
            controller.ModelState.Clear();

            // The model would've been validated and would not be null, this will force an Uncaught Exception
            HttpResponseMessage result = await controller.PostAsync(null);
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, result.StatusCode, "Unexpected error not returning 500 Internal Server Error");
        }

        /// <summary>
        /// Checks whether a valid model will return 200 OK
        /// </summary>
        [TestMethod]
        public async Task ValidModel()
        {
            var model = new EventUpdateModel()
            {
                eventID = "123",
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether an existing event receives the new properties
        /// </summary>
        [TestMethod]
        public async Task ExistingEvent_Properties()
        {
            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID.ToString(),
                eventTitle = random.Next(1, 10000).ToString(),
                alertRadius = (random.Next(1, 10000) % 2 == 0).ToString(),
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                endDate = DateTime.Now.AddMinutes(random.Next(1, 10000)).ToString(),
                lastUpdatedDate = DateTime.Now.AddMinutes(random.Next(1, 10000)).ToString(),
                priorityID = random.Next(1, 10000).ToString(),
                summary = random.Next(1, 10000).ToString(),
                notes = random.Next(1, 10000).ToString(),
                diseaseID = random.Next(1, 10000).ToString(),
                speciesID = random.Next(1, 10000),
                eventMongoId = random.Next(1, 10000).ToString(),
                LastUpdatedByUserName = random.Next(1, 10000).ToString()
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var existingEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID);

            // Check properties are set
            Assert.AreEqual(model.eventID, existingEvent.EventId.ToString(), "Event ID not properly updated");
            Assert.AreEqual(model.eventTitle, existingEvent.EventTitle, "Event Title not properly updated");
            Assert.AreEqual(model.alertRadius, existingEvent.IsLocalOnly.ToString(), "Alert Radius not properly updated");
            Assert.AreEqual(model.startDate, existingEvent.StartDate.ToString(), "Start Date not properly updated");
            Assert.AreEqual(model.endDate, existingEvent.EndDate.ToString(), "End Date not properly updated");
            Assert.AreEqual(model.priorityID, existingEvent.PriorityId?.ToString(), "Priority ID not properly updated");
            Assert.AreEqual(model.summary, existingEvent.Summary, "Summary not properly updated");
            Assert.AreEqual(model.notes, existingEvent.Notes, "Notes not properly updated");
            Assert.AreEqual(model.diseaseID, existingEvent.DiseaseId.ToString(), "Disease ID not properly updated");
            Assert.AreEqual(model.speciesID, existingEvent.SpeciesId, "Species ID not properly updated");
            Assert.AreEqual(model.eventMongoId, existingEvent.EventMongoId, "Event Mongo ID not properly updated");
            Assert.AreEqual(model.LastUpdatedByUserName, existingEvent.LastUpdatedByUserName, "Last Updated By User Name not properly updated");
            Assert.AreEqual(true, existingEvent.IsPublished, "Is Published flag not set to default of true");
            Assert.IsNotNull(existingEvent.LastUpdatedDate, "Last Updated Date not set as a default to current date time");
        }

        /// <summary>
        /// Checks whether an existing event receives new reasons
        /// </summary>
        [TestMethod]
        public async Task ExistingEvent_Reasons()
        {
            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[] { "101", "102", "103", "104", "105" },
                startDate = DateTime.Now.ToString()
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var existingEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID);

            // Check existing reasons were cleared
            Assert.AreEqual(0, existingEvent.EventCreationReasons.Where(cr => cr.ReasonName.StartsWith(ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID.ToString())).Count(), "Existing Reasons were not cleared on update");

            // Check new reasons are added
            HashSet<string> initialReasonIds = new HashSet<string>(model.reasonIDs);
            HashSet<string> resultReasonIds = new HashSet<string>(existingEvent.EventCreationReasons.Select(cr => cr.ReasonId.ToString()));

            Assert.AreEqual(initialReasonIds.Count, existingEvent.EventCreationReasons.Count, "New creation reasons not properly added");
            Assert.AreEqual(0, initialReasonIds.Except(resultReasonIds).Count(), "Not all reason IDs sent in the request were added");
            Assert.AreEqual(0, resultReasonIds.Except(initialReasonIds).Count(), "Unexpected reason IDs added that were not part of the request");
        }

        /// <summary>
        /// Checks whether an existing event receives new articles
        /// </summary>
        [TestMethod]
        public async Task ExistingEvent_Articles()
        {
            var articles = new List<ArticleUpdateForZebra>()
            {
                // Existing Articles
                new ArticleUpdateForZebra()
                {
                    ArticleId = "101"
                },
                new ArticleUpdateForZebra()
                {
                    ArticleId = "102"
                },

                // New Articles to be created
                new ArticleUpdateForZebra()
                {
                    ArticleId = "103"
                },
                new ArticleUpdateForZebra()
                {
                    ArticleId = "104"
                }
            };

            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                associatedArticles = JsonConvert.SerializeObject(articles)
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var existingEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID);

            // Check existing articles were cleared
            Assert.AreEqual(0, existingEvent.ProcessedArticles.Where(a => a.ArticleId.StartsWith(ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID.ToString())).Count(), "Existing Articles were not cleared on update");

            // Check new articles are added
            HashSet<string> initialArticleIds = new HashSet<string>(articles.Select(a => a.ArticleId));
            HashSet<string> resultArticleIds = new HashSet<string>(existingEvent.ProcessedArticles.Select(a => a.ArticleId));

            Assert.AreEqual(initialArticleIds.Count, existingEvent.ProcessedArticles.Count, "New articles not properly added");
            Assert.AreEqual(0, initialArticleIds.Except(resultArticleIds).Count(), "Not all article IDs sent in the request were added");
            Assert.AreEqual(0, resultArticleIds.Except(initialArticleIds).Count(), "Unexpected article IDs added that were not part of the request");
        }

        /// <summary>
        /// Checks whether an existing event receives new articles
        /// </summary>
        [TestMethod]
        public async Task ExistingEvent_EventLocations()
        {
            var locations = new List<EventLocation>()
            {
                new EventLocation()
                {
                    GeonameId = 101
                },
                new EventLocation()
                {
                    GeonameId = 102
                },
                new EventLocation()
                {
                    GeonameId = 103
                },
                new EventLocation()
                {
                    GeonameId = 104
                },
                new EventLocation()
                {
                    GeonameId = 105
                },
                new EventLocation()
                {
                    GeonameId = 106
                },
                new EventLocation()
                {
                    GeonameId = 107
                },
                new EventLocation()
                {
                    GeonameId = 108
                },
            };
            HashSet<int> initialGeonameIds = new HashSet<int>(locations.Select(l => l.GeonameId));

            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                locationObject = JsonConvert.SerializeObject(locations)
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var existingEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID);

            // Check existing event locations were cleared
            Assert.AreEqual(0, existingEvent.Xtbl_Event_Location.Where(el => el.EventId == ZebraEventUpdateMockDbSet.EXISTING_EVENT_ID && !initialGeonameIds.Contains(el.GeonameId)).Count(), "Existing Event Locations were not cleared on update");

            // Check new event locations are added
            HashSet<int> resultGeonameIds = new HashSet<int>(existingEvent.Xtbl_Event_Location.Select(el => el.GeonameId));

            Assert.AreEqual(initialGeonameIds.Count, existingEvent.Xtbl_Event_Location.Count, "New event locations not properly added");
            Assert.AreEqual(0, initialGeonameIds.Except(resultGeonameIds).Count(), "Not all event locations sent in the request were added");
            Assert.AreEqual(0, resultGeonameIds.Except(initialGeonameIds).Count(), "Unexpected event locations added that were not part of the request");
        }

        /// <summary>
        /// Checks whether a new event is created with the new properties
        /// </summary>
        [TestMethod]
        public async Task NewEvent_Properties()
        {
            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.NEW_EVENT_ID.ToString(),
                eventTitle = random.Next(1, 10000).ToString(),
                alertRadius = (random.Next(1, 10000) % 2 == 0).ToString(),
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                endDate = DateTime.Now.AddMinutes(random.Next(1, 10000)).ToString(),
                lastUpdatedDate = DateTime.Now.AddMinutes(random.Next(1, 10000)).ToString(),
                priorityID = random.Next(1, 10000).ToString(),
                summary = random.Next(1, 10000).ToString(),
                notes = random.Next(1, 10000).ToString(),
                diseaseID = random.Next(1, 10000).ToString(),
                speciesID = random.Next(1, 10000),
                eventMongoId = random.Next(1, 10000).ToString(),
                LastUpdatedByUserName = random.Next(1, 10000).ToString()
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var resultEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.NEW_EVENT_ID);

            // Check properties are set
            Assert.AreEqual(model.eventID, resultEvent.EventId.ToString(), "Event ID not properly updated");
            Assert.AreEqual(model.eventTitle, resultEvent.EventTitle, "Event Title not properly updated");
            Assert.AreEqual(model.alertRadius, resultEvent.IsLocalOnly.ToString(), "Alert Radius not properly updated");
            Assert.AreEqual(model.startDate, resultEvent.StartDate.ToString(), "Start Date not properly updated");
            Assert.AreEqual(model.endDate, resultEvent.EndDate.ToString(), "End Date not properly updated");
            Assert.AreEqual(model.priorityID, resultEvent.PriorityId?.ToString(), "Priority ID not properly updated");
            Assert.AreEqual(model.summary, resultEvent.Summary, "Summary not properly updated");
            Assert.AreEqual(model.notes, resultEvent.Notes, "Notes not properly updated");
            Assert.AreEqual(model.diseaseID, resultEvent.DiseaseId.ToString(), "Disease ID not properly updated");
            Assert.AreEqual(model.speciesID, resultEvent.SpeciesId, "Species ID not properly updated");
            Assert.AreEqual(model.eventMongoId, resultEvent.EventMongoId, "Event Mongo ID not properly updated");
            Assert.AreEqual(model.LastUpdatedByUserName, resultEvent.LastUpdatedByUserName, "Last Updated By User Name not properly updated");
            Assert.AreEqual(true, resultEvent.IsPublished, "Is Published flag not set to default of true");
            Assert.IsNotNull(resultEvent.LastUpdatedDate, "Last Updated Date not set as a default to current date time");
            Assert.IsNotNull(resultEvent.CreatedDate, "Created Date not set as a default to current date time");
        }

        /// <summary>
        /// Checks whether a new event associates to the reasons
        /// </summary>
        [TestMethod]
        public async Task NewEvent_Reasons()
        {
            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.NEW_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[] { "101", "102", "103", "104", "105" },
                startDate = DateTime.Now.ToString()
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var resultEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.NEW_EVENT_ID);

            // Check new reasons are added
            HashSet<string> initialReasonIds = new HashSet<string>(model.reasonIDs);
            HashSet<string> resultReasonIds = new HashSet<string>(resultEvent.EventCreationReasons.Select(cr => cr.ReasonId.ToString()));

            Assert.AreEqual(initialReasonIds.Count, resultEvent.EventCreationReasons.Count, "New creation reasons not properly added");
            Assert.AreEqual(0, initialReasonIds.Except(resultReasonIds).Count(), "Not all reason IDs sent in the request were added");
            Assert.AreEqual(0, resultReasonIds.Except(initialReasonIds).Count(), "Unexpected reason IDs added that were not part of the request");
        }

        /// <summary>
        /// Checks whether a new event associates to the articles
        /// </summary>
        [TestMethod]
        public async Task NewEvent_Articles()
        {
            var articles = new List<ArticleUpdateForZebra>()
            {
                // Existing Articles
                new ArticleUpdateForZebra()
                {
                    ArticleId = "101"
                },
                new ArticleUpdateForZebra()
                {
                    ArticleId = "102"
                },

                // New Articles to be created
                new ArticleUpdateForZebra()
                {
                    ArticleId = "103"
                },
                new ArticleUpdateForZebra()
                {
                    ArticleId = "104"
                }
            };

            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.NEW_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                associatedArticles = JsonConvert.SerializeObject(articles)
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var resultEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.NEW_EVENT_ID);

            // Check new articles are added
            HashSet<string> initialArticleIds = new HashSet<string>(articles.Select(a => a.ArticleId));
            HashSet<string> resultArticleIds = new HashSet<string>(resultEvent.ProcessedArticles.Select(a => a.ArticleId));

            Assert.AreEqual(initialArticleIds.Count, resultEvent.ProcessedArticles.Count, "New articles not properly added");
            Assert.AreEqual(0, initialArticleIds.Except(resultArticleIds).Count(), "Not all article IDs sent in the request were added");
            Assert.AreEqual(0, resultArticleIds.Except(initialArticleIds).Count(), "Unexpected article IDs added that were not part of the request");
        }

        /// <summary>
        /// Checks whether a new event associates to the event locations
        /// </summary>
        [TestMethod]
        public async Task NewEvent_EventLocations()
        {
            var locations = new List<EventLocation>()
            {
                new EventLocation()
                {
                    GeonameId = 101
                },
                new EventLocation()
                {
                    GeonameId = 102
                },
                new EventLocation()
                {
                    GeonameId = 103
                },
                new EventLocation()
                {
                    GeonameId = 104
                },
                new EventLocation()
                {
                    GeonameId = 105
                },
                new EventLocation()
                {
                    GeonameId = 106
                },
                new EventLocation()
                {
                    GeonameId = 107
                },
                new EventLocation()
                {
                    GeonameId = 108
                },
            };
            HashSet<int> initialGeonameIds = new HashSet<int>(locations.Select(l => l.GeonameId));

            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.NEW_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                locationObject = JsonConvert.SerializeObject(locations)
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var resultEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.NEW_EVENT_ID);

            // Check new event locations are added
            HashSet<int> resultGeonameIds = new HashSet<int>(resultEvent.Xtbl_Event_Location.Select(el => el.GeonameId));

            Assert.AreEqual(initialGeonameIds.Count, resultEvent.Xtbl_Event_Location.Count, "New event locations not properly added");
            Assert.AreEqual(0, initialGeonameIds.Except(resultGeonameIds).Count(), "Not all event locations sent in the request were added");
            Assert.AreEqual(0, resultGeonameIds.Except(initialGeonameIds).Count(), "Unexpected event locations added that were not part of the request");
        }

        /// <summary>
        /// Checks whether a new article sent as part of the request has all the properties created correctly
        /// </summary>
        [TestMethod]
        public async Task NewArticle_Properties()
        {
            var article = new ArticleUpdateForZebra()
            {
                ArticleId = random.Next(1, 10000).ToString(),
                ArticleTitle = random.Next(1, 10000).ToString(),
                SystemLastModifiedDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                CertaintyScore = (decimal)random.NextDouble(),
                ArticleFeedId = random.Next(1, 10000),
                FeedURL = random.Next(1, 10000).ToString(),
                FeedSourceId = random.Next(1, 10000).ToString(),
                FeedPublishedDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                HamTypeId = random.Next(1, 10000),
                OriginalSourceURL = random.Next(1, 10000).ToString(),
                IsCompleted = random.Next(1, 10000) % 2 == 0,
                SimilarClusterId = (decimal)random.NextDouble(),
                OriginalLanguage = random.Next(1, 10000).ToString(),
                UserLastModifiedDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                LastUpdatedByUserName = random.Next(1, 10000).ToString(),
                Notes = random.Next(1, 10000).ToString(),
                ArticleBody = random.Next(1, 10000).ToString(),
                IsRead = random.Next(1, 10000) % 2 == 0
            };

            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.NEW_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                associatedArticles = JsonConvert.SerializeObject(new List<ArticleUpdateForZebra>() { article })
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var resultEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.NEW_EVENT_ID);
            var resultArticle = resultEvent.ProcessedArticles.FirstOrDefault();

            Assert.IsNotNull(resultArticle, "New article with properties was not properly associated to the event");

            // Check properties are set
            Assert.AreEqual(article.ArticleId, resultArticle.ArticleId, "Article ID not properly saved");
            Assert.AreEqual(article.ArticleTitle, resultArticle.ArticleTitle, "Article Title not properly saved");
            Assert.AreEqual(article.SystemLastModifiedDate, resultArticle.SystemLastModifiedDate, "System Last Modified Date not properly saved");
            Assert.AreEqual(article.CertaintyScore, resultArticle.CertaintyScore, "Certainty Score not properly saved");
            Assert.AreEqual(article.ArticleFeedId, resultArticle.ArticleFeedId, "Article Feed ID not properly saved");
            Assert.AreEqual(article.FeedURL, resultArticle.FeedURL, "Feed URL not properly saved");
            Assert.AreEqual(article.FeedSourceId, resultArticle.FeedSourceId, "Feed Source ID not properly saved");
            Assert.AreEqual(article.FeedPublishedDate, resultArticle.FeedPublishedDate, "Feed Published Date not properly saved");
            Assert.AreEqual(article.HamTypeId, resultArticle.HamTypeId, "Ham Type ID not properly saved");
            Assert.AreEqual(article.OriginalSourceURL, resultArticle.OriginalSourceURL, "Original Source URL not properly saved");
            Assert.AreEqual(article.IsCompleted, resultArticle.IsCompleted, "Is Completed not properly saved");
            Assert.AreEqual(article.SimilarClusterId, resultArticle.SimilarClusterId, "Similar Cluster ID not properly saved");
            Assert.AreEqual(article.OriginalLanguage, resultArticle.OriginalLanguage, "Original Language not properly saved");
            Assert.AreEqual(article.UserLastModifiedDate, resultArticle.UserLastModifiedDate, "User Last Modified Date not properly saved");
            Assert.AreEqual(article.LastUpdatedByUserName, resultArticle.LastUpdatedByUserName, "Last Updated By User Name not properly saved");
            Assert.AreEqual(article.Notes, resultArticle.Notes, "Notes not properly saved");
            Assert.AreEqual(article.ArticleBody, resultArticle.ArticleBody, "Article Body not properly saved");
            Assert.AreEqual(article.IsRead, resultArticle.IsRead, "Is Read not properly saved");
        }

        /// <summary>
        /// Checks whether a new event location sent as part of the request has all the properties created correctly
        /// </summary>
        [TestMethod]
        public async Task NewEventLocation_Properties()
        {
            var eventLocation = new EventLocation()
            {
                GeonameId = random.Next(1, 10000),
                EventDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                Deaths = random.Next(1, 10000),
                ConfCases = random.Next(1, 10000),
                RepCases = random.Next(1, 10000),
                SuspCases = random.Next(1, 10000)
            };

            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.NEW_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                locationObject = JsonConvert.SerializeObject(new List<EventLocation>() { eventLocation })
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            var resultEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.NEW_EVENT_ID);
            var resultEventLocation = resultEvent.Xtbl_Event_Location.FirstOrDefault();

            Assert.IsNotNull(resultEventLocation, "New event location with properties was not properly associated to the event");

            // Check properties are set
            Assert.AreEqual(eventLocation.GeonameId, resultEventLocation.GeonameId, "Geoname ID not properly saved");
            Assert.AreEqual(eventLocation.EventDate, resultEventLocation.EventDate, "Event Date not properly saved");
            Assert.AreEqual(eventLocation.Deaths, resultEventLocation.Deaths, "Deaths not properly saved");
            Assert.AreEqual(eventLocation.ConfCases, resultEventLocation.ConfCases, "Conf Cases not properly saved");
            Assert.AreEqual(eventLocation.RepCases, resultEventLocation.RepCases, "Rep Cases not properly saved");
            Assert.AreEqual(eventLocation.SuspCases, resultEventLocation.SuspCases, "Susp Cases not properly saved");
        }

        /// <summary>
        /// Checks whether a request with multiple event location with the same geoname ids only 
        /// saves the one with the most recent event date
        /// </summary>
        [TestMethod]
        public async Task EventLocation_LatestOnly()
        {
            var latestEventDate = DateTime.Now.AddMinutes(random.Next(10000, 20000));

            var locations = new List<EventLocation>()
            {
                new EventLocation()
                {
                    GeonameId = 101,
                    EventDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                    Deaths = random.Next(1, 10000),
                    ConfCases = random.Next(1, 10000),
                    RepCases = random.Next(1, 10000),
                    SuspCases = random.Next(1, 10000)
                },
                new EventLocation()
                {
                    GeonameId = 101,
                    EventDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                    Deaths = random.Next(1, 10000),
                    ConfCases = random.Next(1, 10000),
                    RepCases = random.Next(1, 10000),
                    SuspCases = random.Next(1, 10000)
                },
                new EventLocation()
                {
                    GeonameId = 101,
                    EventDate = latestEventDate,
                    Deaths = random.Next(1, 10000),
                    ConfCases = random.Next(1, 10000),
                    RepCases = random.Next(1, 10000),
                    SuspCases = random.Next(1, 10000)
                },
                new EventLocation()
                {
                    GeonameId = 101,
                    EventDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                    Deaths = random.Next(1, 10000),
                    ConfCases = random.Next(1, 10000),
                    RepCases = random.Next(1, 10000),
                    SuspCases = random.Next(1, 10000)
                }
            };

            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.NEW_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString(),
                locationObject = JsonConvert.SerializeObject(locations)
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);

            // Check whether only the latest was saved
            var resultEvent = mockDbContext.Object.Events.First(e => e.EventId == ZebraEventUpdateMockDbSet.NEW_EVENT_ID);
            Assert.AreEqual(1, resultEvent.Xtbl_Event_Location.Count, "Multiple event locations added with the same geoname id");

            var resultEventLocation = resultEvent.Xtbl_Event_Location.First();
            Assert.AreEqual(101, resultEventLocation.GeonameId, "Saved event location is not the right geoname id");
            Assert.AreEqual(latestEventDate, resultEventLocation.EventDate, "Saved event location is not the event location with the most recent event date");
        }

        /// <summary>
        /// Checks whether the returned stored procedure values for a MinCasesOverPopulationSize is over 0 is unmodified
        /// </summary>
        [TestMethod]
        public async Task Event_MinCasesOverPopulationSize_OverZero()
        {
            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.MIN_CASE_OVER_ZERO_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether the returned stored procedure values for a MinCasesOverPopulationSize is equal 0 is unmodified
        /// </summary>
        [TestMethod]
        public async Task Event_MinCasesOverPopulationSize_EqualZero()
        {
            var model = new EventUpdateModel()
            {
                eventID = ZebraEventUpdateMockDbSet.MIN_CASE_EQUAL_ZERO_EVENT_ID.ToString(),
                speciesID = 1,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            HttpResponseMessage result = await controller.PostAsync(model);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode, "Successful request not returning 200 OK");
        }

        /// <summary>
        /// Checks whether IsEventLocationChanged can test not changed case
        /// </summary>
        [TestMethod]
        public void IsEventLocationChanged_NotChanged()
        {
            var oldLocations = new List<Xtbl_Event_Location>()
            {
                new Xtbl_Event_Location()
                {
                    GeonameId = 101,
                    EventDate = new DateTime(2019,10,01),
                    SuspCases = 1,
                    ConfCases = 2,
                    RepCases = 3
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 102,
                    EventDate = new DateTime(2019,11,01),
                    SuspCases = 2,
                    ConfCases = 3,
                    RepCases = 5
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 103,
                    EventDate = new DateTime(2019,12,01),
                    SuspCases = 3,
                    ConfCases = 5,
                    RepCases = 8
                },
            };
            var newLocations = new List<Xtbl_Event_Location>()
            {
                new Xtbl_Event_Location()
                {
                    GeonameId = 102,
                    EventDate = new DateTime(2019,11,01),
                    SuspCases = 2,
                    ConfCases = 3,
                    RepCases = 5
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 101,
                    EventDate = new DateTime(2019,10,01),
                    SuspCases = 1,
                    ConfCases = 2,
                    RepCases = 3
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 103,
                    EventDate = new DateTime(2019,12,01),
                    SuspCases = 3,
                    ConfCases = 5,
                    RepCases = 8
                },
            };

            var result = controller.IsEventLocationChanged(newLocations, oldLocations);
            Assert.IsFalse(result);

        }
        /// <summary>
        /// Checks whether IsEventLocationChanged can test changed case
        /// </summary>
        [TestMethod]
        public void IsEventLocationChanged_Changed()
        {
            var oldLocations = new List<Xtbl_Event_Location>()
            {
                new Xtbl_Event_Location()
                {
                    GeonameId = 101,
                    EventDate = new DateTime(2019,10,01),
                    SuspCases = 1,
                    ConfCases = 2,
                    RepCases = 3
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 102,
                    EventDate = new DateTime(2019,11,01),
                    SuspCases = 2,
                    ConfCases = 3,
                    RepCases = 5
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 103,
                    EventDate = new DateTime(2019,12,01),
                    SuspCases = 3,
                    ConfCases = 5,
                    RepCases = 8
                },
            };
            var newLocationsDate = new List<Xtbl_Event_Location>()
            {
                new Xtbl_Event_Location()
                {
                    GeonameId = 101,
                    EventDate = new DateTime(2019,10,01),
                    SuspCases = 1,
                    ConfCases = 2,
                    RepCases = 3
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 102,
                    EventDate = new DateTime(2019,11,02),
                    SuspCases = 2,
                    ConfCases = 3,
                    RepCases = 5
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 103,
                    EventDate = new DateTime(2019,12,01),
                    SuspCases = 3,
                    ConfCases = 5,
                    RepCases = 8
                },
            };
            var newLocationsCase = new List<Xtbl_Event_Location>()
            {
                new Xtbl_Event_Location()
                {
                    GeonameId = 101,
                    EventDate = new DateTime(2019,10,01),
                    SuspCases = 1,
                    ConfCases = 2,
                    RepCases = 3
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 102,
                    EventDate = new DateTime(2019,11,01),
                    SuspCases = 2,
                    ConfCases = 3,
                    RepCases = 5
                },
                new Xtbl_Event_Location()
                {
                    GeonameId = 103,
                    EventDate = new DateTime(2019,12,01),
                    SuspCases = 3,
                    ConfCases = 5,
                    RepCases = 9
                },
            };

            var result = controller.IsEventLocationChanged(newLocationsDate, oldLocations);
            Assert.IsTrue(result);

            result = controller.IsEventLocationChanged(newLocationsCase, oldLocations);
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void GetEventHashCode_Unchanged()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            var event2 = event1;
            var hash2 = controller.GetEventHashCode(event2);
            
            Assert.AreEqual(hash1, hash2, "Same object returning different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_StartDate()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            event1.StartDate = event1.StartDate.AddDays(3);
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Modified start date should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EndDate()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            event1.EndDate = event1.StartDate.AddDays(3);
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Modified end date should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_DiseaseId()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            event1.DiseaseId = 200;
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Modified disease id should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_SpeciesId()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            event1.SpeciesId = 200;
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Modified species id should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_IsLocalOnly()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            event1.IsLocalOnly = !event1.IsLocalOnly;
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Modified is local only flag should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            event1.Xtbl_Event_Location = new List<Xtbl_Event_Location>();
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreEqual(hash1, hash2, "Same empty event locations returning different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_AddedLocation()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            var hash1 = controller.GetEventHashCode(event1);

            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Added event location should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_RemovedLocation()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash1 = controller.GetEventHashCode(event1);
            
            event1.Xtbl_Event_Location = new List<Xtbl_Event_Location>();
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Removed event location should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_SameLocation()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash1 = controller.GetEventHashCode(event1);
            
            event1.Xtbl_Event_Location = new List<Xtbl_Event_Location>
            {
                new Xtbl_Event_Location
                {
                    GeonameId = 101,
                    EventDate = new DateTime(2019,10,01),
                    SuspCases = 1,
                    ConfCases = 2,
                    RepCases = 3,
                    Deaths = 4
                }
            };
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreEqual(hash1, hash2, "Same event location returning different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_GeonameId()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash1 = controller.GetEventHashCode(event1);

            event1.Xtbl_Event_Location.First().GeonameId = 2323;
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Event location with different geoname id should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_EventDate()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash1 = controller.GetEventHashCode(event1);

            event1.Xtbl_Event_Location.First().EventDate = new DateTime(2020,10,01);
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Event location with different date should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_SuspCases()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash1 = controller.GetEventHashCode(event1);

            event1.Xtbl_Event_Location.First().SuspCases = 5;
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Event location with different date should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_ConfCases()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash1 = controller.GetEventHashCode(event1);

            event1.Xtbl_Event_Location.First().ConfCases = 6;
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Event location with different date should return different hashes");
        }
        
        [TestMethod]
        public void GetEventHashCode_EventLocation_RepCases()
        {
            var event1 = ZebraEventUpdateMockDbSet.EVENT_HASH_TEST;
            event1.Xtbl_Event_Location.Add(new Xtbl_Event_Location
            {
                GeonameId = 101,
                EventDate = new DateTime(2019,10,01),
                SuspCases = 1,
                ConfCases = 2,
                RepCases = 3,
                Deaths = 4
            });
            var hash1 = controller.GetEventHashCode(event1);

            event1.Xtbl_Event_Location.First().RepCases = 7;
            var hash2 = controller.GetEventHashCode(event1);
            
            Assert.AreNotEqual(hash1, hash2, "Event location with different date should return different hashes");
        }
    }
}
