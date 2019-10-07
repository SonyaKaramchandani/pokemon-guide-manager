using Biod.Zebra.Library.EntityModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace Biod.Solution.UnitTest.Api
{
    class ZebraNotificationMockDbSet
    {
        private static readonly Random random = new Random();

        public Mock<BiodZebraEntities> MockContext { get; }

        public ZebraNotificationMockDbSet()
        {
            MockContext = new Mock<BiodZebraEntities>();

            MockContext.Setup(context => context.usp_ZebraEventGetArticlesByEventId(It.IsAny<int>())).Returns(CreateEmptyMockArticles());
            MockContext.Setup(context => context.usp_ZebraDashboardGetOutbreakPotentialCategories()).Returns(CreateMockOutbreakCategories());
            MockContext.Setup(context => context.UserExternalIds).ReturnsDbSet(new List<UserExternalId>());
        }

        private ObjectResult<usp_ZebraEventGetArticlesByEventId_Result> CreateEmptyMockArticles()
        {
            var mockResult = new Mock<ObjectResult<usp_ZebraEventGetArticlesByEventId_Result>>();
            mockResult.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEventGetArticlesByEventId_Result>().GetEnumerator());

            return mockResult.Object;
        }

        private ObjectResult<usp_ZebraDashboardGetOutbreakPotentialCategories_Result> CreateMockOutbreakCategories()
        {
            var mockCategoryList = new List<usp_ZebraDashboardGetOutbreakPotentialCategories_Result>
            {
                new usp_ZebraDashboardGetOutbreakPotentialCategories_Result
                {
                    AttributeId = 1,
                    EffectiveMessage = "Sustained",
                    EffectiveMessageDescription = "Sustained",
                    IsLocalTransmissionPossible = true,
                    MapThreshold = null,
                    NeedsMap = false,
                    Rule = "4"
                },
                new usp_ZebraDashboardGetOutbreakPotentialCategories_Result
                {
                    AttributeId = 5,
                    EffectiveMessage = "Unknown",
                    EffectiveMessageDescription = "Unknown",
                    IsLocalTransmissionPossible = false,
                    MapThreshold = null,
                    NeedsMap = false,
                    Rule = "1"
                }

            };

            var mockResult = new Mock<ObjectResult<usp_ZebraDashboardGetOutbreakPotentialCategories_Result>>();
            mockResult.Setup(x => x.GetEnumerator()).Returns(mockCategoryList.GetEnumerator());

            return mockResult.Object;
        }
    }
}
