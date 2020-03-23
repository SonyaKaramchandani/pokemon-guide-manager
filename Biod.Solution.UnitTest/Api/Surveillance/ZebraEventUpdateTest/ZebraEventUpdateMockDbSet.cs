using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Biod.Solution.UnitTest.MockDbContext;

namespace Biod.Solution.UnitTest.Api
{
    class ZebraEventUpdateMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly int EXISTING_EVENT_ID = 56483653;
        public static readonly int NEW_EVENT_ID = 23409871;

        // Preset expected values for the case of MinCaseOverPop > 0
        public static readonly int MIN_CASE_OVER_ZERO_EVENT_ID = 98572957;
        public static readonly int MIN_CASE_OVER_ZERO_GRID_ID = random.Next(10000, 50000);
        public static readonly int MIN_CASE_OVER_ZERO_CASES = random.Next(50, 300);
        public static readonly double MIN_CASE_OVER_ZERO_MIN_CASE_OVER_POP = random.NextDouble() + 0.01;
        public static readonly double MIN_CASE_OVER_ZERO_MAX_CASE_OVER_POP = random.NextDouble() + random.Next(100, 200);
        public static readonly DateTime MIN_CASE_OVER_ZERO_EVENT_START = DateTime.SpecifyKind(DateTime.Now.AddDays(random.Next(1, 100)), DateTimeKind.Unspecified);
        public static readonly DateTime MIN_CASE_OVER_ZERO_EVENT_END = DateTime.SpecifyKind(DateTime.Now.AddDays(random.Next(100, 900)), DateTimeKind.Unspecified);
        public static readonly decimal MIN_CASE_OVER_ZERO_DISEASE_INCUBATION = (decimal) random.NextDouble();
        public static readonly decimal MIN_CASE_OVER_ZERO_DISEASE_SYMPTOMATIC = (decimal) random.NextDouble();

        // Preset expected values for the case of MinCaseOverPop = 0
        public static readonly int MIN_CASE_EQUAL_ZERO_EVENT_ID = 59827928;
        public static readonly int MIN_CASE_EQUAL_ZERO_GRID_ID = random.Next(10000, 50000);
        public static readonly int MIN_CASE_EQUAL_ZERO_CASES = random.Next(50, 300);
        public static readonly double MIN_CASE_EQUAL_ZERO_MIN_CASE_OVER_POP = 0d;
        public static readonly double MIN_CASE_EQUAL_ZERO_MAX_CASE_OVER_POP = random.NextDouble() + random.Next(100, 200);
        public static readonly DateTime MIN_CASE_EQUAL_ZERO_EVENT_START = DateTime.SpecifyKind(DateTime.Now.AddDays(random.Next(1, 100)), DateTimeKind.Unspecified);
        public static readonly DateTime MIN_CASE_EQUAL_ZERO_EVENT_END = DateTime.SpecifyKind(DateTime.Now.AddDays(random.Next(100, 900)), DateTimeKind.Unspecified);
        public static readonly decimal MIN_CASE_EQUAL_ZERO_DISEASE_INCUBATION = (decimal) random.NextDouble();
        public static readonly decimal MIN_CASE_EQUAL_ZERO_DISEASE_SYMPTOMATIC = (decimal) random.NextDouble();

        public static Event EVENT_HASH_TEST =>
            new Event
            {
                EventId = 459278359,
                StartDate = DateTime.SpecifyKind(DateTime.Now.AddDays(random.Next(1, 100)), DateTimeKind.Unspecified),
                EndDate = null,
                DiseaseId = random.Next(1, 100),
                SpeciesId = random.Next(1, 100),
                IsLocalOnly = random.Next(50, 300) % 2 == 0,
                Xtbl_Event_Location = new List<Xtbl_Event_Location>()
            };

        public Mock<BiodZebraEntities> MockContext { get; set; }

        public ZebraEventUpdateMockDbSet()
        {
            var mockedEvents = CreateMockedEvents();
            var creationReasons = CreateMockedEventCreationReasons();
            var articles = CreateProcessedArticles();
            var eventLocations = CreateEventLocations();
            var eventSourceAirportSpreadMds = CreateEventSourceAirportSpreadMds();

            // Join data between mocked tables
            ConfigureExistingEvent(mockedEvents, creationReasons, articles, eventLocations);

            // Initialize a Mock DB with test data
            MockContext = new Mock<BiodZebraEntities>();
            MockContext.Setup(context => context.Events).ReturnsDbSet(mockedEvents);
            MockContext.Setup(context => context.EventCreationReasons).ReturnsDbSet(creationReasons);
            MockContext.Setup(context => context.ProcessedArticles).ReturnsDbSet(articles);
            MockContext.Setup(context => context.Xtbl_Event_Location).ReturnsDbSet(eventLocations);
            MockContext.Setup(context => context.EventSourceGridSpreadMds).ReturnsDbSet(new List<EventSourceGridSpreadMd>());
            MockContext.Setup(context => context.EventSourceAirportSpreadMds).ReturnsDbSet(eventSourceAirportSpreadMds);

            // Mocked SP
            MockContext.Setup(context => context.usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd(It.IsAny<int?>()))
                .Returns((int? eventId) => SetZebraSourceDestinationsV6Part1(eventId));
            MockContext.Setup(context => context.usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd(It.IsAny<int?>()))
                .Returns((int? eventId) => SetZebraSourceDestinationsV6Part2(eventId));
            MockContext.Setup(context => context.usp_ZebraDataRenderSetSourceDestinationsPart3SpreadMd(It.IsAny<int?>()))
                .Returns((int? eventId) => SetZebraSourceDestinationsV6Part3(eventId));
            MockContext.Setup(context => context.usp_ZebraEventSetEventCase(It.IsAny<int>()))
                .Returns(SetZebraEventCaseCountHistory);
        }

        public static List<Event> CreateMockedEvents()
        {
            return new List<Event>
            {
                new Event()
                {
                    EventId = EXISTING_EVENT_ID
                }
            };
        }

        public static List<EventCreationReason> CreateMockedEventCreationReasons()
        {
            return new List<EventCreationReason>()
            {
                // Reasons for existing Event ID
                new EventCreationReason()
                {
                    ReasonId = 1,
                    ReasonName = EXISTING_EVENT_ID.ToString() + ": 1"
                },
                new EventCreationReason()
                {
                    ReasonId = 2,
                    ReasonName = EXISTING_EVENT_ID.ToString() + ": 2"
                },

                // Reasons to be associated to events, sent as part of the request
                new EventCreationReason()
                {
                    ReasonId = 101,
                    ReasonName = "101"
                },
                new EventCreationReason()
                {
                    ReasonId = 102,
                    ReasonName = "102"
                },
                new EventCreationReason()
                {
                    ReasonId = 103,
                    ReasonName = "103"
                },
                new EventCreationReason()
                {
                    ReasonId = 104,
                    ReasonName = "104"
                },
                new EventCreationReason()
                {
                    ReasonId = 105,
                    ReasonName = "105"
                },
            };
        }

        public static List<ProcessedArticle> CreateProcessedArticles()
        {
            return new List<ProcessedArticle>()
            {
                // Articles for existing Event ID
                new ProcessedArticle()
                {
                    ArticleId = EXISTING_EVENT_ID.ToString() + ": 1"
                },
                new ProcessedArticle()
                {
                    ArticleId = EXISTING_EVENT_ID.ToString() + ": 2"
                },

                // Articles to be associated to events, sent as part of the request
                new ProcessedArticle()
                {
                    ArticleId = "101"
                },
                new ProcessedArticle()
                {
                    ArticleId = "102"
                }
                // Article ID 103 and 104 are reserved for creation from the request
            };
        }

        public static List<Xtbl_Event_Location> CreateEventLocations()
        {
            return new List<Xtbl_Event_Location>()
            {
                new Xtbl_Event_Location()
                {
                    EventId = EXISTING_EVENT_ID,
                    GeonameId = 1
                },
                new Xtbl_Event_Location()
                {
                    EventId = EXISTING_EVENT_ID,
                    GeonameId = 2
                }

                // Geoname ID 101 to 108 reserved for creation from the request
            };
        }

        public static List<EventSourceAirportSpreadMd> CreateEventSourceAirportSpreadMds()
        {
            return new List<EventSourceAirportSpreadMd>()
            {
                new EventSourceAirportSpreadMd()
                {
                    EventId = MIN_CASE_OVER_ZERO_EVENT_ID,
                    MinCaseOverPop = 0.5
                },
                new EventSourceAirportSpreadMd()
                {
                    EventId = MIN_CASE_EQUAL_ZERO_EVENT_ID,
                    MinCaseOverPop = 0
                }
            };
        }

        public static ObjectResult<usp_ZebraEventSetEventCase_Result> SetZebraEventCaseCountHistory()
        {
            var result = new Mock<TestableObjectResult<usp_ZebraEventSetEventCase_Result>>();
            var resultList = new List<usp_ZebraEventSetEventCase_Result>();
            resultList.Add(new usp_ZebraEventSetEventCase_Result()
            {
                Result = true
            });

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraEventSetEventCase_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        public static ObjectResult<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result> SetZebraSourceDestinationsV6Part1(int? eventId)
        {
            var result = new Mock<TestableObjectResult<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result>>();
            var resultList = new List<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result>();

            // Preset values for specific event ids, with default random values
            if (eventId == MIN_CASE_OVER_ZERO_EVENT_ID)
            {
                resultList.Add(new usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result()
                {
                    GridId = MIN_CASE_OVER_ZERO_GRID_ID.ToString(),
                    Cases = MIN_CASE_OVER_ZERO_CASES
                });
            }
            else if (eventId == MIN_CASE_EQUAL_ZERO_EVENT_ID)
            {
                resultList.Add(new usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result()
                {
                    GridId = MIN_CASE_EQUAL_ZERO_GRID_ID.ToString(),
                    Cases = MIN_CASE_EQUAL_ZERO_CASES
                });
            }
            else
            {
                resultList.Add(new usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result()
                {
                    GridId = random.Next(1, 10000).ToString(),
                    Cases = random.Next(1, 10000)
                });
            }

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        public static ObjectResult<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result> SetZebraSourceDestinationsV6Part1_NoResults(int? eventId)
        {
            var result = new Mock<TestableObjectResult<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result>>();
            var resultList = new List<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result>()
            {
                // This should be ignored
                new usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result()
                {
                    GridId = "-1",
                    Cases = 0
                }
            };

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        public ObjectResult<usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd_Result> SetZebraSourceDestinationsV6Part2(int? eventId)
        {
            var minMaxCases = MockContext.Object.EventSourceGridSpreadMds.First(e => e.EventId == eventId);
            
            var result = new Mock<TestableObjectResult<usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd_Result>>();
            var resultList = new List<usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd_Result>();

            if (eventId == MIN_CASE_OVER_ZERO_EVENT_ID)
            {
                // Validate what was returned is unchanged since the service call
                Assert.AreEqual(MIN_CASE_OVER_ZERO_GRID_ID.ToString(), minMaxCases.GridId);
                Assert.AreEqual(MIN_CASE_OVER_ZERO_CASES.ToString(), minMaxCases.Cases);
                Assert.AreEqual((MIN_CASE_OVER_ZERO_GRID_ID + MIN_CASE_OVER_ZERO_CASES).ToString(), minMaxCases.MinCases);
                Assert.AreEqual((MIN_CASE_OVER_ZERO_GRID_ID * MIN_CASE_OVER_ZERO_CASES).ToString(), minMaxCases.MaxCases);

                // Send the Min Case to be over 0
                resultList.Add(new usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd_Result()
                {
                    EventStart = MIN_CASE_OVER_ZERO_EVENT_START,
                    EventEnd = MIN_CASE_OVER_ZERO_EVENT_END,
                    DiseaseIncubation = MIN_CASE_OVER_ZERO_DISEASE_INCUBATION,
                    DiseaseSymptomatic = MIN_CASE_OVER_ZERO_DISEASE_SYMPTOMATIC
                });
            }
            else if (eventId == MIN_CASE_EQUAL_ZERO_EVENT_ID)
            {
                // Validate what was returned is unchanged since the service call
                Assert.AreEqual(MIN_CASE_EQUAL_ZERO_GRID_ID.ToString(), minMaxCases.GridId);
                Assert.AreEqual(MIN_CASE_EQUAL_ZERO_CASES.ToString(), minMaxCases.Cases);
                Assert.AreEqual((MIN_CASE_EQUAL_ZERO_GRID_ID + MIN_CASE_EQUAL_ZERO_CASES).ToString(), minMaxCases.MinCases);
                Assert.AreEqual((MIN_CASE_EQUAL_ZERO_GRID_ID * MIN_CASE_EQUAL_ZERO_CASES).ToString(), minMaxCases.MaxCases);

                // Send the Min Case to be equal 0
                resultList.Add(new usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd_Result()
                {
                    EventStart = MIN_CASE_EQUAL_ZERO_EVENT_START,
                    EventEnd = MIN_CASE_EQUAL_ZERO_EVENT_END,
                    DiseaseIncubation = MIN_CASE_EQUAL_ZERO_DISEASE_INCUBATION,
                    DiseaseSymptomatic = MIN_CASE_EQUAL_ZERO_DISEASE_SYMPTOMATIC
                });
            }
            else
            {
                resultList.Add(new usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd_Result()
                {
                    EventStart = DateTime.Now,
                    EventEnd = DateTime.SpecifyKind(DateTime.Now.AddMinutes(random.Next(1, 10000)), DateTimeKind.Unspecified),
                    DiseaseIncubation = (decimal) random.NextDouble(),
                    DiseaseSymptomatic = (decimal) random.NextDouble()
                });
            }

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        public ObjectResult<int?> SetZebraSourceDestinationsV6Part3(int? eventId)
        {
            var prevalence = MockContext.Object.EventSourceAirportSpreadMds.FirstOrDefault(e => e.EventId == eventId);
            if (eventId == MIN_CASE_OVER_ZERO_EVENT_ID)
            {
                var minPrevalenceCalc = MIN_CASE_OVER_ZERO_MIN_CASE_OVER_POP + MIN_CASE_OVER_ZERO_MAX_CASE_OVER_POP + (double) MIN_CASE_OVER_ZERO_DISEASE_INCUBATION + (double) MIN_CASE_OVER_ZERO_DISEASE_SYMPTOMATIC;
                Assert.IsTrue(Math.Abs((prevalence.MinPrevalence ?? 0) - minPrevalenceCalc) < 0.00001);
                Assert.AreEqual(Math.Round((MIN_CASE_OVER_ZERO_EVENT_END - MIN_CASE_OVER_ZERO_EVENT_START).TotalDays), Math.Round(prevalence.MaxPrevalence ?? 0));
            }
            else if (eventId == MIN_CASE_EQUAL_ZERO_EVENT_ID)
            {
                Assert.AreEqual(0, prevalence.MinPrevalence);
                Assert.AreEqual(Math.Round((MIN_CASE_EQUAL_ZERO_EVENT_END - MIN_CASE_EQUAL_ZERO_EVENT_START).TotalDays), Math.Round(prevalence.MaxPrevalence ?? 0));
            }

            var result = new Mock<TestableObjectResult<int?>>();
            var resultList = new List<int?>()
            {
                random.Next(1, 10000)
            };

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<int?>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        /// <summary>
        /// Helper function to create the EF associations of the Event to the related reasons, articles, and event locations
        /// </summary>
        private static void ConfigureExistingEvent(List<Event> events, List<EventCreationReason> creationReasons, List<ProcessedArticle> articles, List<Xtbl_Event_Location> eventLocations)
        {
            Event existingEvent = events.First(e => e.EventId == EXISTING_EVENT_ID);
            foreach (var reason in creationReasons.Where(cr => cr.ReasonName.StartsWith(EXISTING_EVENT_ID.ToString())).AsEnumerable())
            {
                existingEvent.EventCreationReasons.Add(reason);
            }

            foreach (var article in articles.Where(a => a.ArticleId.StartsWith(EXISTING_EVENT_ID.ToString())).AsEnumerable())
            {
                existingEvent.ProcessedArticles.Add(article);
            }

            foreach (var el in eventLocations.Where(el => el.EventId == EXISTING_EVENT_ID).AsEnumerable())
            {
                existingEvent.Xtbl_Event_Location.Add(el);
            }
        }
    }
}