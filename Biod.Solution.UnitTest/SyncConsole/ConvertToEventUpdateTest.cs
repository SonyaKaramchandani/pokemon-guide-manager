using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biod.Surveillance.Zebra.SyncConsole.EntityModels;
using Biod.Surveillance.Zebra.SyncConsole;
using Biod.Surveillance.Zebra.SyncConsole.Models;
using Moq;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Biod.Solution.UnitTest.SyncConsole
{
    /// <summary>
    /// Tests the ConvertToEventUpdate method in the SyncConsole program
    /// </summary>
    [TestClass]
    public class ConvertToEventUpdateTest
    {
        private Mock<BiodSurveillanceDataModelEntities> mockDbContext;
        private MockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new MockDbSet();
            mockDbContext = dbMock.MockContext;
        }

        /// <summary>
        /// Tests whether the exception is thrown when null is passed for the event model
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A null event was inappropriately allowed")]
        public void NullObject()
        {
            Program.ConvertToEventUpdate(mockDbContext.Object, null);
        }

        /// <summary>
        /// Tests whether all non-entity fields are correctly converted to the output model
        /// </summary>
        [TestMethod]
        public void AllFields()
        {
            Event publishedEvent = mockDbContext.Object.Events.First(e => e.EventId == MockDbSet.FULL_FIELD_EVENT_ID);

            EventUpdateModel result = Program.ConvertToEventUpdate(mockDbContext.Object, publishedEvent);

            Assert.AreEqual(result.eventID, publishedEvent.EventId, "EventId not mapped correctly");
            Assert.AreEqual(result.eventTitle, publishedEvent.EventTitle, "EventTitle not mapped correctly");
            Assert.AreEqual(result.startDate, publishedEvent.StartDate.ToString(), "StartDate not mapped correctly");
            Assert.AreEqual(result.endDate, publishedEvent.EndDate.ToString(), "EndDate not mapped correctly");
            Assert.AreEqual(result.lastUpdatedDate, publishedEvent.LastUpdatedDate.ToString(), "LastUpdatedDate not mapped correctly");
            Assert.AreEqual(result.diseaseID, publishedEvent.DiseaseId.ToString(), "DiseaseId not mapped correctly");
            Assert.AreEqual(result.speciesID, publishedEvent.SpeciesId, "SpeciesId not mapped correctly");
            Assert.AreEqual(result.alertRadius, publishedEvent.IsLocalOnly.ToString(), "IsLocalOnly not mapped correctly");
            Assert.AreEqual(result.priorityID, publishedEvent.PriorityId.ToString(), "PriorityId not mapped correctly");
            Assert.AreEqual(result.isPublished, publishedEvent.IsPublished.ToString(), "IsPublished not mapped correctly");
            Assert.AreEqual(result.summary, publishedEvent.Summary, "Summary not mapped correctly");
            Assert.AreEqual(result.notes, publishedEvent.Notes, "Notes not mapped correctly");
            Assert.AreEqual(result.LastUpdatedByUserName, publishedEvent.LastUpdatedByUserName, "LastUpdatedByUserName not mapped correctly");
        }

        /// <summary>
        /// Tests whether an empty table for related Event Location entities return the correct empty array
        /// </summary>
        [TestMethod]
        public void EmptyEventLocationField()
        {
            Event publishedEvent = mockDbContext.Object.Events.First(e => e.EventId == MockDbSet.FULL_FIELD_EVENT_ID);

            EventUpdateModel result = Program.ConvertToEventUpdate(mockDbContext.Object, publishedEvent);

            Assert.AreEqual(result.locationObject, "[]", "LocationObject should be empty when there are no Xtbl_Event_Location rows");
        }

        /// <summary>
        /// Tests whether the set of related Event Location entities are being returned in the serialized array
        /// </summary>
        [TestMethod]
        public void MultipleEventLocationField()
        {
            Event publishedEvent = mockDbContext.Object.Events.First(e => e.EventId == MockDbSet.EVENT_LOCATION_EVENT_ID);
            HashSet<int> geonameIds = new HashSet<int>(
                mockDbContext.Object.Xtbl_Event_Location
                    .Where(l => l.EventId == MockDbSet.EVENT_LOCATION_EVENT_ID)
                    .Select(l => l.GeonameId)
                    .AsEnumerable()
            );

            EventUpdateModel result = Program.ConvertToEventUpdate(mockDbContext.Object, publishedEvent);
            HashSet<int> resultGeonameIds = new HashSet<int>(
                JsonConvert.DeserializeObject<List<EventLocation>>(result.locationObject)
                    .Select(l => l.GeonameId)
                    .AsEnumerable()
            );

            Assert.AreEqual(geonameIds.Except(resultGeonameIds).Count(), 0, "Event locations in the database are missing after mapping");
            Assert.AreEqual(resultGeonameIds.Except(geonameIds).Count(), 0, "Unknown event locations not in the database are added after mapping");
        }

        /// <summary>
        /// Tests whether an empty table for related Event Creation Reason entities return an empty array
        /// </summary>
        [TestMethod]
        public void EmptyEventCreationReasons()
        {
            Event publishedEvent = mockDbContext.Object.Events.First(e => e.EventId == MockDbSet.FULL_FIELD_EVENT_ID);

            EventUpdateModel result = Program.ConvertToEventUpdate(mockDbContext.Object, publishedEvent);

            Assert.IsNotNull(result.reasonIDs, "Reason IDs should not be null, even when there are no associated reasons");
            Assert.AreEqual(result.reasonIDs.Count(), 0, "Reason IDs should be empty when there are no associated reasons");
        }

        /// <summary>
        /// Tests whether the set of related Event Creation Reason entities are being returned
        /// </summary>
        [TestMethod]
        public void MultipleEventCreationReasons()
        {
            Event publishedEvent = mockDbContext.Object.Events.First(e => e.EventId == MockDbSet.CREATION_REASON_EVENT_ID);
            HashSet<int> reasonIds = new HashSet<int>(
                publishedEvent.EventCreationReasons
                    .Select(r => r.ReasonId)
                    .AsEnumerable()
            );

            EventUpdateModel result = Program.ConvertToEventUpdate(mockDbContext.Object, publishedEvent);
            HashSet<int> resultReasonIds = new HashSet<int>(
                result.reasonIDs
                    .Select(r => Convert.ToInt32(r))
                    .AsEnumerable()
            );

            Assert.AreEqual(reasonIds.Except(resultReasonIds).Count(), 0, "Reason ids in the original event are missing after mapping");
            Assert.AreEqual(resultReasonIds.Except(reasonIds).Count(), 0, "Unknown reason ids not in the original event are added after mapping");
        }

        /// <summary>
        /// Tests whether an empty table for related Processed Article entities return the correct empty array
        /// </summary>
        [TestMethod]
        public void EmptyProcessedArticles()
        {
            Event publishedEvent = mockDbContext.Object.Events.First(e => e.EventId == MockDbSet.FULL_FIELD_EVENT_ID);

            EventUpdateModel result = Program.ConvertToEventUpdate(mockDbContext.Object, publishedEvent);

            Assert.AreEqual(result.associatedArticles, "[]", "AssociatedArticles should be empty when there are no processed articles");
        }

        /// <summary>
        /// Tests whether the set of related Processed Article entities are being returned in the serialized array
        /// </summary>
        [TestMethod]
        public void MultipleProcessedArticles()
        {
            Event publishedEvent = mockDbContext.Object.Events.First(e => e.EventId == MockDbSet.PROCESSED_ARTICLE_EVENT_ID);
            HashSet<string> articleIds = new HashSet<string>(
                publishedEvent.ProcessedArticles
                    .Select(a => a.ArticleId)
                    .AsEnumerable()
            );

            EventUpdateModel result = Program.ConvertToEventUpdate(mockDbContext.Object, publishedEvent);
            HashSet<string> resultArticleIds = new HashSet<string>(
                JsonConvert.DeserializeObject<List<ProcessedArticle>>(result.associatedArticles)
                    .Select(a => a.ArticleId)
                    .AsEnumerable()
            );

            Assert.AreEqual(articleIds.Except(resultArticleIds).Count(), 0, "Article ids in the original event are missing after mapping");
            Assert.AreEqual(resultArticleIds.Except(articleIds).Count(), 0, "Unknown article ids not in the original event are added after mapping");
        }
    }
}
