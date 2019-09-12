using Biod.Zebra.Library.EntityModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using static Biod.Solution.UnitTest.MockDbContext;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    class ZebraAnalyticsEventMockDbSet
    {
        private static readonly Random random = new Random();

        public static readonly int EVENT_1_ID = random.Next(1000000, 9999999);
        public static readonly int EVENT_NULL_FIELDS_ID = random.Next(1000000, 9999999);

        public static readonly usp_ZebraAnalyticsGetEventByEventId_Result EVENT_1 = new usp_ZebraAnalyticsGetEventByEventId_Result()
        {
            EventId = EVENT_1_ID,
            EventTitle = random.Next(1000000, 9999999).ToString(),
            Brief = random.Next(1000000, 9999999).ToString(),
            DiseaseName = random.Next(1000000, 9999999).ToString(),
            MicrobeType = random.Next(1000000, 9999999).ToString(),
            Reasons = random.Next(1000000, 9999999).ToString(),
            IncubationPeriod = random.Next(1000000, 9999999).ToString(),
            PriorityTitle = random.Next(1000000, 9999999).ToString(),
            ProbabilityName = random.Next(1000000, 9999999).ToString(),
            TransmittedBy = random.Next(1000000, 9999999).ToString(),
            Vaccination = random.Next(1000000, 9999999).ToString(),
            CaseConf = random.Next(1000000, 9999999),
            CasesRpted = random.Next(1000000, 9999999),
            CaseSusp = random.Next(1000000, 9999999),
            Deaths = random.Next(1000000, 9999999),
            LastUpdatedDate = DateTime.Now.AddDays(random.Next(1, 100)),
            StartDate = DateTime.Now.AddDays(random.Next(1, 100)),
            EndDate = DateTime.Now.AddDays(random.Next(1, 100))
        };
        public static readonly usp_ZebraAnalyticsGetEventByEventId_Result EVENT_NULL_FIELDS = new usp_ZebraAnalyticsGetEventByEventId_Result()
        {
            EventId = EVENT_NULL_FIELDS_ID
        };

        public static readonly Dictionary<int, usp_ZebraAnalyticsGetEventByEventId_Result> EVENT_DICT = new Dictionary<int, usp_ZebraAnalyticsGetEventByEventId_Result>()
        {
            { EVENT_1_ID, EVENT_1 },
            { EVENT_NULL_FIELDS_ID, EVENT_NULL_FIELDS }
        };

        public Mock<BiodZebraEntities> MockContext { get; set; }

        public ZebraAnalyticsEventMockDbSet()
        {
            MockContext = new Mock<BiodZebraEntities>();

            MockContext.Setup(context => context.usp_ZebraAnalyticsGetEventByEventId(It.IsAny<int?>()))
                .Returns((int? eventId) => { return GetZebraEventDetailInfoByEventId(eventId); });
        }

        private ObjectResult<usp_ZebraAnalyticsGetEventByEventId_Result> GetZebraEventDetailInfoByEventId(int? eventId)
        {
            var result = new Mock<TestableObjectResult<usp_ZebraAnalyticsGetEventByEventId_Result>>();
            var resultList = new List<usp_ZebraAnalyticsGetEventByEventId_Result>();

            bool found = EVENT_DICT.TryGetValue((int) eventId, out usp_ZebraAnalyticsGetEventByEventId_Result matchedEvent);
            if (found)
            {
                resultList.Add(matchedEvent);
            }

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraAnalyticsGetEventByEventId_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }
    }
}
