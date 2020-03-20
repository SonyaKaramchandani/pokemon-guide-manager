using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Moq;
using System;
using System.Collections.Generic;

namespace Biod.Solution.UnitTest.Api.Surveillance.ZebraNotificationsTest
{
    class ProximalMockDbSet
    {
        private static readonly Random random = new Random();

        private Mock<BiodZebraEntities> MockContext { get; }

        private HashSet<Xtbl_Event_Location_history> EventLocationsHistory = new HashSet<Xtbl_Event_Location_history>();

        private HashSet<Xtbl_Event_Location> EventLocations = new HashSet<Xtbl_Event_Location>();

        private HashSet<ActiveGeoname> Geonames = new HashSet<ActiveGeoname>();

        public ProximalMockDbSet():this(new Mock<BiodZebraEntities>())
        {
        }

        public ProximalMockDbSet(Mock<BiodZebraEntities> dbContext)
        {
            MockContext = dbContext;
        }

        public void AddEventToEventLocationTables(int eventId, int geonameId, int locationType, bool didCaseCountIncrease)
        {
            var caseCounts = new
            {
                SuspCases = random.Next(0, 99999),
                ConfCases = random.Next(0, 99999),
                Deaths = random.Next(0, 99999)
            };
            var eventLocationHistory = new Xtbl_Event_Location_history
            {
                EventId = eventId,
                GeonameId = geonameId,
                EventDate = DateTime.Now,
                SuspCases = caseCounts.SuspCases,
                ConfCases = caseCounts.ConfCases,
                Deaths = caseCounts.Deaths,
                RepCases = caseCounts.SuspCases + caseCounts.ConfCases + caseCounts.Deaths,
                EventDateType = Constants.LocationHistoryDataType.PROXIMAL_DATA,
                ActiveGeoname = new ActiveGeoname
                {
                    LocationType = locationType,
                    GeonameId = geonameId,
                    Admin1GeonameId = locationType == Constants.LocationType.COUNTRY ? (int?)null : locationType == Constants.LocationType.PROVINCE ? geonameId : random.Next(),
                    CountryGeonameId = locationType == Constants.LocationType.COUNTRY ? geonameId : random.Next(),
                    Name = $"{geonameId}",
                    DisplayName = $"{geonameId}"
                }
            };

            var updatedCaseCounts = new
            {
                SuspCases = Math.Max(0, caseCounts.SuspCases + (didCaseCountIncrease ? 1 : -1)* random.Next(1, 10)),
                ConfCases = Math.Max(0, caseCounts.ConfCases + (didCaseCountIncrease ? 1 : -1) * random.Next(1, 10)),
                Deaths = Math.Max(0, caseCounts.Deaths + (didCaseCountIncrease ? 1 : -1) * random.Next(1, 10))
            };
            var eventLocation = new Xtbl_Event_Location
            {
                EventId = eventLocationHistory.EventId,
                GeonameId = eventLocationHistory.GeonameId,
                EventDate = DateTime.SpecifyKind(DateTime.Now.AddDays(random.NextDouble()*10), DateTimeKind.Unspecified),
                SuspCases = updatedCaseCounts.SuspCases,
                ConfCases = updatedCaseCounts.ConfCases,
                Deaths = updatedCaseCounts.Deaths,
                RepCases = updatedCaseCounts.SuspCases + updatedCaseCounts.ConfCases + updatedCaseCounts.Deaths,
                ActiveGeoname = eventLocationHistory.ActiveGeoname
            };

            EventLocationsHistory.Add(eventLocationHistory);
            EventLocations.Add(eventLocation);
            Geonames.Add(eventLocationHistory.ActiveGeoname);

            MockContext.Setup(context => context.Xtbl_Event_Location_history).ReturnsDbSet(EventLocationsHistory);
            MockContext.Setup(context => context.Xtbl_Event_Location).ReturnsDbSet(EventLocations);
            MockContext.Setup(context => context.ActiveGeonames).ReturnsDbSet(Geonames);
        }
    }
}