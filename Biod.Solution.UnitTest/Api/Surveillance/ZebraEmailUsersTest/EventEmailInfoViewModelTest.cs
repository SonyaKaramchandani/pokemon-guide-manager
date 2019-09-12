//using Biod.Zebra.Api.Models;
using Biod.Zebra.Library.EntityModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Biod.Zebra.Library.Models.Notification;

//using System.Text;
//using System.Threading.Tasks;

namespace Biod.Solution.UnitTest.Api
{
    [TestClass()]
    public class EventEmailInfoViewModelTest
    {
        private Mock<BiodZebraEntities> mockDbContext;
        private ZebraEmailUsersMockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new ZebraEmailUsersMockDbSet();
            mockDbContext = dbMock.MockContext;
        }

        //[TestMethod()]
        public void GetEventEmailInfoModelLocalTest()
        {
            //Arrange
            var eventId = 1924;
            //var eventDiseaseName = "Dengue";
            List<NotificationViewModel> eventEmailInfoVMs = EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, eventId, true);

            //Assert
            //var diseaseName = eventEmailInfoVMs.FirstOrDefault().DiseaseName;
            //Assert.AreEqual(eventDiseaseName, diseaseName);       
            Assert.IsNotNull(eventEmailInfoVMs, "eventEmailInfoVMs is null");

        }

        //[TestMethod()]
        public void GetEventEmailInfoModelLocalGlobalTest()
        {
            //Arrange
            var eventId = 1565;
            List<NotificationViewModel> eventEmailInfoVMs = EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, eventId, false);

            //Assert
            //var diseaseName = eventEmailInfoVMs.FirstOrDefault().DiseaseName;
            //Assert.AreEqual(eventDiseaseName, diseaseName);       
            Assert.IsNotNull(eventEmailInfoVMs, "eventEmailInfoVMs is null");

        }
    }
}
