using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Models.Event;
using Xunit;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class OrderingHelperTest
    {
        #region OrderEventsByDefault

        [Theory]
        [MemberData(nameof(OrderingHelperTestData.OrderEventData), MemberType = typeof(OrderingHelperTestData))]
        public void OrderEventsByDefault(IEnumerable<GetEventModel> eventModels, int[] expectedEventIdOrder)
        {
            var result = OrderingHelper.OrderEventsByDefault(eventModels);
            Assert.Equal(expectedEventIdOrder, result.Select(e => e.EventInformation.Id).ToArray());
        }

        #endregion

        #region OrderLocationsByDefault

        [Theory]
        [MemberData(nameof(OrderingHelperTestData.OrderLocationData), MemberType = typeof(OrderingHelperTestData))]
        public void OrderLocationsByDefault_CountryOnly(IEnumerable<EventLocationModel> eventLocations, int[] expectedGeonameIdOrder)
        {
            var result = OrderingHelper.OrderLocationsByDefault(eventLocations);
            Assert.Equal(expectedGeonameIdOrder, result.Select(r => r.GeonameId).ToArray());
        }

        #endregion
    }
}