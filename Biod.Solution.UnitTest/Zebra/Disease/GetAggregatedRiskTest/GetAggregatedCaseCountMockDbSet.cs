using Biod.Zebra.Library.EntityModels.Zebra;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using static Biod.Solution.UnitTest.MockDbContext;

namespace Biod.Solution.UnitTest.Zebra.Disease.GetAggregatedRiskTest
{
    class GetAggregatedCaseCountMockDbSet
    {
        public static readonly int NULL_RESULT_DISEASE_ID = 1;
        public static readonly int ZERO_RESULT_DISEASE_ID = 2;
        public static readonly int TRAVELLERS_SMALL_RESULT_DISEASE_ID = 3;
        public static readonly int TRAVELLERS_LARGE_RESULT_DISEASE_ID = 4;
        public static readonly int TOTAL_CASE_SMALL_RESULT_DISEASE_ID = 5;
        public static readonly int TOTAL_CASE_LARGE_RESULT_DISEASE_ID = 6;

        public static readonly int TOTAL_CASE_SMALL = 123;
        public static readonly int TOTAL_CASE_LARGE = 1234567;
        public static readonly decimal MIN_TRAVELLERS_SMALL = 0.001m;
        public static readonly decimal MAX_TRAVELLERS_SMALL = 0.005m;
        public static readonly decimal MIN_TRAVELLERS_LARGE = 0.01m;
        public static readonly decimal MAX_TRAVELLERS_LARGE = 0.05m;

        public static readonly int NULL_RESULT_EVENT_ID = 1;

        public Mock<BiodZebraEntities> MockContext { get; set; }

        public GetAggregatedCaseCountMockDbSet()
        {
            MockContext = new Mock<BiodZebraEntities>();
            MockContext.Setup(context => context.usp_ZebraDiseaseGetImportationRisk(It.IsAny<int>(),It.IsAny<string>()))
                .Returns((int diseaseId, string geonameIds) => ZebraDiseaseGetImportationRisk(diseaseId, geonameIds));
            MockContext.Setup(context => context.usp_ZebraDiseaseGetLocalCaseCount(It.IsAny<int>(),It.IsAny<string>(), It.IsAny<int>()))
                .Returns((int diseaseId, string geonameIds, int eventId) => ZebraDiseaseGetLocalCaseCount(diseaseId, geonameIds, eventId));
        }

        private ObjectResult<usp_ZebraDiseaseGetImportationRisk_Result> ZebraDiseaseGetImportationRisk(int diseaseId, string geonameIds)
        {
            var result = new Mock<TestableObjectResult<usp_ZebraDiseaseGetImportationRisk_Result>>();
            var resultList = new List<usp_ZebraDiseaseGetImportationRisk_Result>();

            if (diseaseId == NULL_RESULT_DISEASE_ID)
            {
                resultList.Add(new usp_ZebraDiseaseGetImportationRisk_Result
                {
                    ImportationMaxProbability = null,
                    ImportationMinProbability = null,
                    ImportationMaxExpTravelers = null,
                    ImportationMinExpTravelers = null
                });
            }
            else if (diseaseId == ZERO_RESULT_DISEASE_ID)
            {
                resultList.Add(new usp_ZebraDiseaseGetImportationRisk_Result
                {
                    ImportationMaxProbability = 0,
                    ImportationMinProbability = 0,
                    ImportationMaxExpTravelers = 0,
                    ImportationMinExpTravelers = 0
                });
            }
            else if (diseaseId == TRAVELLERS_SMALL_RESULT_DISEASE_ID)
            {
                resultList.Add(new usp_ZebraDiseaseGetImportationRisk_Result
                {
                    ImportationMaxProbability = MAX_TRAVELLERS_SMALL,
                    ImportationMinProbability = MIN_TRAVELLERS_SMALL,
                    ImportationMaxExpTravelers = MAX_TRAVELLERS_SMALL,
                    ImportationMinExpTravelers = MIN_TRAVELLERS_SMALL
                });
            }
            else if (diseaseId == TRAVELLERS_LARGE_RESULT_DISEASE_ID)
            {
                resultList.Add(new usp_ZebraDiseaseGetImportationRisk_Result
                {
                    ImportationMaxProbability = MAX_TRAVELLERS_LARGE,
                    ImportationMinProbability = MIN_TRAVELLERS_LARGE,
                    ImportationMaxExpTravelers = MAX_TRAVELLERS_LARGE,
                    ImportationMinExpTravelers = MIN_TRAVELLERS_LARGE
                });
            }

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<usp_ZebraDiseaseGetImportationRisk_Result>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }

        private ObjectResult<int?> ZebraDiseaseGetLocalCaseCount(int diseaseId, string geonameIds, int? eventId)
        {
            var result = new Mock<TestableObjectResult<int?>>();
            var resultList = new List<int?>();

            if (diseaseId == NULL_RESULT_DISEASE_ID)
            {
                resultList.Add(null);
            }
            else if (diseaseId == ZERO_RESULT_DISEASE_ID)
            {
                resultList.Add(0);
            }
            else if (diseaseId == TOTAL_CASE_SMALL_RESULT_DISEASE_ID)
            {
                resultList.Add(TOTAL_CASE_SMALL);
            }
            else if (diseaseId == TOTAL_CASE_LARGE_RESULT_DISEASE_ID)
            {
                resultList.Add(TOTAL_CASE_LARGE);
            }

            result.Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());
            result.As<IQueryable<int?>>().Setup(m => m.GetEnumerator()).Returns(() => resultList.GetEnumerator());

            return result.Object;
        }
    }
}
