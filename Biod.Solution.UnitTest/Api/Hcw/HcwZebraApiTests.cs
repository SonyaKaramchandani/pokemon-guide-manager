using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;
using Biod.Zebra.Api.Hcw;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Biod.Solution.UnitTest.Api
{
    [TestClass()]
    public class HcwZebraApiTests
    {
        //[TestMethod()]
        public void TestGetHcwGetDisease_VacicineInfo()
        {
            //Assert.Fail();
            string diseaseId = "55";
            string diseaseName = "Dengue";
            HcwGetDiseaseVaccineInfoController diseaseInfoController = new HcwGetDiseaseVaccineInfoController();

            //Assert
            string expectedDiseaseName = diseaseInfoController.Get(diseaseId).Find(x => x.DiseaseName == diseaseName).DiseaseName;
            Assert.AreEqual(diseaseName, expectedDiseaseName, "Disease names are different");
        }

        //[TestMethod]
        public void TestGetDiseaseDetailInfo_Diseasename()
        {
            //Arrange
            int diseaseId = 55;
            string diseaseName = "Dengue";
            HcwGetDiseaseDetailInfoController diseaseInfoController = new HcwGetDiseaseDetailInfoController();

            //Assert
            string expectedDiseaseName = diseaseInfoController.Get(diseaseId).DiseaseName;
            Assert.AreEqual(diseaseName, expectedDiseaseName, "Disease names are different");

        }

        //[TestMethod]
        public void TestGetDiseaseDetailInfo_Introduction()
        {
            //Arrange
            int diseaseId = 55;
            string diseaseIntro = "Dengue is a <b>viral infection</b> that can cause a wide range of symptoms from mild fever to life-threatening bleeding. <br/>";
            HcwGetDiseaseDetailInfoController diseaseInfoController = new HcwGetDiseaseDetailInfoController();

            //Assert
            string expectedDiseaseIntro = diseaseInfoController.Get(diseaseId).DiseaseIntroduction;
            Assert.AreEqual(diseaseIntro, expectedDiseaseIntro, "Disease names are different");

        }

        //[TestMethod()]
        public void TestHcwGetNextQueryController()
        {
            string expectedResult = "{\"symptom_to_query\": \"0\", \"updated_association_scores\": \"1.0, 0.0, 0.5, 1.0, 1.0, 0.0\"}";
            HcwGetNextQueryController apiController = new HcwGetNextQueryController();
            string result = apiController.Get("0,0,5,0,1,1,1,0,2", "1,0");
            //Assert
            Assert.AreEqual(result, expectedResult, "HcwGetNextQueryController.Get result is not expected");
        }

        //[TestMethod()]
        public void TestHcwPostNextQueryController()
        {
            string expectedResult = "{\"symptom_to_query\": \"0\", \"updated_association_scores\": \"1.0, 0.0, 0.5, 1.0, 1.0, 0.0\"}";
            HcwGetNextQueryController apiController = new HcwGetNextQueryController();
            string result = apiController.Post(
                new Zebra.Api.Hcw.HcwGetNextQueryController.SymptomToQuery()
                {
                    association_score = "0,0,5,0,1,1,1,0,2",
                    symptoms_queried = "1,0"
                }
                );
            //Assert
            Assert.AreEqual(result, expectedResult, "HcwGetNextQueryController.Post result is not expected");
        }

        //[TestMethod()]
        public void HcwGetRiskWithTierController()
        {
            //test
            double[] latitudes = new double[] { 40.71427 };
            double[] longitudes = new double[] { -74.00597 };
            string expectedResult = ",\"latitude\":40.71427,\"longitude\":-74.00597,\"diseaseRisks\":[{\"diseaseRiskId\":1426114141,\"diseaseId\":85,\"url\":\"\",\"seasonal\":true,\"mediaBuzz\":0.0,\"defaultRisk\":{\"level\":{\"phrase\":\"Low\",\"level\":1},\"riskValue\":20.0},\"contextSpecificMessages\":[]},{\"diseaseRiskId\":587253341,\"diseaseId\":35,\"url\":\"\",\"seasonal\":false,\"mediaBuzz\":0.0,\"defaultRisk\":{\"level\":{\"phrase\":\"Low\",\"level\":1},\"riskValue\":5.0},\"contextSpecificMessages\":[]},{\"diseaseRiskId\":167822941,\"diseaseId\":10,\"url\":\"\",\"seasonal\":false,\"mediaBuzz\":0.0,\"defaultRisk\":{\"level\":{\"phrase\":\"Low\",\"level\":1},\"riskValue\":4.0},\"contextSpecificMessages\":[]},{\"diseaseRiskId\":201377373,\"diseaseId\":12,\"url\":\"\",\"seasonal\":false,\"mediaBuzz\":0.0,\"defaultRisk\":{\"level\":{\"phrase\":\"Low\",\"level\":1},\"riskValue\":2.0},\"contextSpecificMessages\":[]}],\"waterQuality\":\"Safe\"}],\"cacheTag\":\"1\"}";
            HcwGetRiskWithTierController apiController = new HcwGetRiskWithTierController();
            IHttpActionResult result = apiController.Get(latitudes, longitudes, 1, "web");
            var contentResult = result as OkNegotiatedContentResult<string>;
            //Assert
            var truncatedResult = contentResult.Content.Replace(contentResult.Content.Substring(0, contentResult.Content.IndexOf(',')), "");
            Assert.AreEqual(truncatedResult, expectedResult, "HcwGetRiskWithTierController.Get result is not expected");
        }
    }
}