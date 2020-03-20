using Biod.Zebra.Library.EntityModels.Surveillance;
using Moq;
using System;
using System.Collections.Generic;

namespace Biod.Solution.UnitTest.SyncConsole
{
    class MockDbSet
    {
        public static readonly int FULL_FIELD_EVENT_ID = 194479;
        public static readonly int EVENT_LOCATION_EVENT_ID = 218719;
        public static readonly int CREATION_REASON_EVENT_ID = 5713517;
        public static readonly int PROCESSED_ARTICLE_EVENT_ID = 5373;
        public static readonly int SPECIES_HUMAN_ID = 1;

        private static readonly Random random = new Random();

        public Mock<BiodSurveillanceDataEntities> MockContext { get; set; }

        public MockDbSet()
        {
            // Initialize a Mock DB with test data
            MockContext = new Mock<BiodSurveillanceDataEntities>();
            MockContext.Setup(context => context.SurveillanceEvents).ReturnsDbSet(CreateMockedEvents());
            MockContext.Setup(context => context.SurveillanceXtbl_Event_Location).ReturnsDbSet(CreateEventLocation());
        }

        public static List<SurveillanceEvent> CreateMockedEvents()
        {
            return new List<SurveillanceEvent>
            {
                // Non-published event
                new SurveillanceEvent()
                {
                    EventId = 1,
                    IsPublished = false,
                    SpeciesId = SPECIES_HUMAN_ID
                },
                new SurveillanceEvent()
                {
                    EventId = 2,
                    IsPublished = false,
                    SpeciesId = SPECIES_HUMAN_ID
                },
                new SurveillanceEvent()
                {
                    EventId = 3,
                    IsPublished = false,
                    SpeciesId = SPECIES_HUMAN_ID
                },

                // Event with all fields filled out
                new SurveillanceEvent()
                {
                    EventId = FULL_FIELD_EVENT_ID,
                    EventTitle = random.Next(1, 10000).ToString(),
                    StartDate = DateTime.SpecifyKind(DateTime.Now.AddMinutes(random.Next(1, 10000)), DateTimeKind.Unspecified),
                    EndDate = DateTime.SpecifyKind(DateTime.Now.AddMinutes(random.Next(20000, 30000)), DateTimeKind.Unspecified),
                    LastUpdatedDate = DateTime.SpecifyKind(DateTime.Now.AddMinutes(random.Next(20000, 30000)), DateTimeKind.Unspecified),
                    DiseaseId = random.Next(1, 10000),
                    SpeciesId = SPECIES_HUMAN_ID,
                    EventCreationReasons = new List<SurveillanceEventCreationReason>() { },
                    IsLocalOnly = random.Next(1, 10000) % 2 == 0,
                    PriorityId = random.Next(1, 10000),
                    Summary = random.Next(1, 10000).ToString(),
                    Notes = random.Next(1, 10000).ToString(),
                    ProcessedArticles = new List<SurveillanceProcessedArticle>() { },
                    LastUpdatedByUserName = random.Next(1, 10000).ToString(),
                    IsPublished = true
                },

                // Event for verifying the Xtbl_Event_Location field
                new SurveillanceEvent()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    IsPublished = true,
                    SpeciesId = SPECIES_HUMAN_ID
                },

                // Event for verifying the Event Creation Reason field
                new SurveillanceEvent()
                {
                    EventId = CREATION_REASON_EVENT_ID,
                    EventCreationReasons = CreateEventCreationReasons(),
                    IsPublished = true,
                    SpeciesId = SPECIES_HUMAN_ID
                },

                // Event for verifying the Processed Article field
                new SurveillanceEvent()
                {
                    EventId = PROCESSED_ARTICLE_EVENT_ID,
                    ProcessedArticles = CreateProcessedArticles(),
                    IsPublished = true,
                    SpeciesId = SPECIES_HUMAN_ID
                }
            };
        }

        public static List<SurveillanceXtbl_Event_Location> CreateEventLocation()
        {
            return new List<SurveillanceXtbl_Event_Location>()
            {
                // Event Location X-table
                new SurveillanceXtbl_Event_Location()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    GeonameId = random.Next(1, 10000)
                },
                new SurveillanceXtbl_Event_Location()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    GeonameId = random.Next(1, 10000)
                },
                new SurveillanceXtbl_Event_Location()
                {
                    EventId = EVENT_LOCATION_EVENT_ID,
                    GeonameId = random.Next(1, 10000)
                },

                // Unrelated/random rows
                new SurveillanceXtbl_Event_Location()
                {
                    EventId = 1,
                    GeonameId = random.Next(1, 10000)
                },
                new SurveillanceXtbl_Event_Location()
                {
                    EventId = 2,
                    GeonameId = random.Next(1, 10000)
                },
                new SurveillanceXtbl_Event_Location()
                {
                    EventId = 3,
                    GeonameId = random.Next(1, 10000)
                },
                new SurveillanceXtbl_Event_Location()
                {
                    EventId = 4,
                    GeonameId = random.Next(1, 10000)
                },
            };
        }

        public static List<SurveillanceEventCreationReason> CreateEventCreationReasons()
        {
            return new List<SurveillanceEventCreationReason>()
            {
                new SurveillanceEventCreationReason() { ReasonId = random.Next(1, 10000) },
                new SurveillanceEventCreationReason() { ReasonId = random.Next(1, 10000) },
                new SurveillanceEventCreationReason() { ReasonId = random.Next(1, 10000) },
                new SurveillanceEventCreationReason() { ReasonId = random.Next(1, 10000) }
            };
        }

        public static List<SurveillanceProcessedArticle> CreateProcessedArticles()
        {
            return new List<SurveillanceProcessedArticle>()
            {
                new SurveillanceProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new SurveillanceProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new SurveillanceProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new SurveillanceProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new SurveillanceProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new SurveillanceProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() },
                new SurveillanceProcessedArticle() { ArticleId = random.Next(1, 10000).ToString() }
            };
        }
    }
}
