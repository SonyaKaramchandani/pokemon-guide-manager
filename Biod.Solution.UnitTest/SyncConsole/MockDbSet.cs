using Biod.Surveillance.Zebra.SyncConsole.EntityModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Solution.UnitTest.SyncConsole
{
    class MockDbSet
    {
        public static readonly int FULL_FIELD_EVENT_ID = 194479;
        public static readonly int EVENT_LOCATION_EVENT_ID = 218719;
        public static readonly int CREATION_REASON_EVENT_ID = 5713517;
        public static readonly int PROCESSED_ARTICLE_EVENT_ID = 5373;

        private static readonly Random random = new Random();

        public Mock<BiodSurveillanceDataModelEntities> MockContext { get; set; }

        public MockDbSet()
        {
            // Initialize a Mock DB with test data
            MockContext = new Mock<BiodSurveillanceDataModelEntities>();
            MockContext.Setup(context => context.Events).ReturnsDbSet(CreateMockedEvents());
            MockContext.Setup(context => context.Xtbl_Event_Location).ReturnsDbSet(CreateEventLocation());
        }

        public static List<Event> CreateMockedEvents()
        {
            return new List<Event>
            {
                // Non-published event
                new Event()
                {
                    EventId = 1,
                    IsPublished = false
                },
                new Event()
                {
                    EventId = 2,
                    IsPublished = false
                },
                new Event()
                {
                    EventId = 3,
                    IsPublished = false
                },

                // Event with all fields filled out
                new Event()
                {
                    EventId = FULL_FIELD_EVENT_ID,
                    EventTitle = random.Next(1, 10000).ToString(),
                    StartDate = DateTime.Now.AddMinutes(random.Next(1, 10000)),
                    EndDate = DateTime.Now.AddMinutes(random.Next(20000, 30000)),
                    DiseaseId = random.Next(1, 10000),
                    EventCreationReasons = new List<EventCreationReason>() { },
                    IsLocalOnly = random.Next(1, 10000) % 2 == 0,
                    PriorityId = random.Next(1, 10000),
                    Summary = random.Next(1, 10000).ToString(),
                    Notes = random.Next(1, 10000).ToString(),
                    ProcessedArticles = new List<ProcessedArticle>() { },
                    LastUpdatedByUserName = random.Next(1, 10000).ToString(),
                    IsPublished = true
                },

                // Event for verifying the Xtbl_Event_Location field
                new Event()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    IsPublished = true
                },

                // Event for verifying the Event Creation Reason field
                new Event()
                {
                    EventId = CREATION_REASON_EVENT_ID,
                    EventCreationReasons = CreateEventCreationReasons(),
                    IsPublished = true
                },

                // Event for verifying the Processed Article field
                new Event()
                {
                    EventId = PROCESSED_ARTICLE_EVENT_ID,
                    ProcessedArticles = CreateProcessedArticles(),
                    IsPublished = true
                }
            };
        }

        public static List<Xtbl_Event_Location> CreateEventLocation()
        {
            return new List<Xtbl_Event_Location>()
            {
                // Event Location X-table
                new Xtbl_Event_Location()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    GeonameId = random.Next(1, 10000)
                },
                new Xtbl_Event_Location()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    GeonameId = random.Next(1, 10000)
                },
                new Xtbl_Event_Location()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    GeonameId = random.Next(1, 10000)
                },

                // Unrelated/random rows
                new Xtbl_Event_Location()
                {
                    EventId = 1,
                    GeonameId = random.Next(1, 10000)
                },
                new Xtbl_Event_Location()
                {
                    EventId = 2,
                    GeonameId = random.Next(1, 10000)
                },
                new Xtbl_Event_Location()
                {
                    EventId = 3,
                    GeonameId = random.Next(1, 10000)
                },
                new Xtbl_Event_Location()
                {
                    EventId = 4,
                    GeonameId = random.Next(1, 10000)
                },
            };
        }

        public static List<EventCreationReason> CreateEventCreationReasons()
        {
            return new List<EventCreationReason>()
            {
                new EventCreationReason() { ReasonId = random.Next(1, 10000) },
                new EventCreationReason() { ReasonId = random.Next(1, 10000) },
                new EventCreationReason() { ReasonId = random.Next(1, 10000) },
                new EventCreationReason() { ReasonId = random.Next(1, 10000) }
            };
        }

        public static List<ProcessedArticle> CreateProcessedArticles()
        {
            return new List<ProcessedArticle>()
            {
                new ProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new ProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new ProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new ProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new ProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new ProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new ProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() }
            };
        }
    }
}
