using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Biod.Zebra.Api;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Biod.Zebra.Api.Hcw.UnitTest.Api
{
    [TestClass()]
    public class HcwZebraApiTests2
    {
        [TestMethod()]
        public void TestGetHcwGetDisease_VacicineInfo()
        {
            //Assert.Fail();
        }

        [TestMethod]
        public void TestGetDiseaseDetailInfo_Diseasename()
        {
            //Arrange
            int diseaseId = 55;
            string diseaseName = "Dengue";
            Biod.Zebra.Api.HcwGetDiseaseDetailInfoController diseaseInfoController = new Biod.Zebra.Api.HcwGetDiseaseDetailInfoController();

            //Assert
            string expectedDiseaseName = diseaseInfoController.Get(diseaseId).DiseaseName;
            Assert.AreEqual(diseaseName, expectedDiseaseName, "Disease names are different");

        }

        [TestMethod]
        public void TestGetDiseaseDetailInfo_Introduction()
        {
            //Arrange
            int diseaseId = 55;
            string diseaseIntro = "Dengue is a <b>viral infection</b> that can cause a wide range of symptoms from mild fever to life-threatening bleeding. <br/>";
            Biod.Zebra.Api.HcwGetDiseaseDetailInfoController diseaseInfoController = new Biod.Zebra.Api.HcwGetDiseaseDetailInfoController();

            //Assert
            string expectedDiseaseIntro = diseaseInfoController.Get(diseaseId).DiseaseIntroduction;
            Assert.AreEqual(diseaseIntro, expectedDiseaseIntro, "Disease names are different");

        }
    }
}