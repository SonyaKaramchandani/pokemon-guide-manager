using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Notification;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Biod.Solution.UnitTest.Api.Surveillance.ZebraNotificationsTest
{
    /// <summary>
    /// Tests lists of <c>NotificationViewModel</c> obtained from <c>ProximalVliewModel.GetNotificationViewModelList</c>
    /// </summary>
    [TestClass()]
    public class ProximalViewModelTest : NotificationTest
    {
        [TestInitialize()]
        public override void Initialize()
        {
            base.Initialize();

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object);
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { }.AsQueryable());
        }

        [TestMethod()]
        public void EmptyProximalEvents()
        {
            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event to User with Unconfirmed E-mail"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });
            var mockProximalEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetProximalEmailData_Result>>();
            mockProximalEmailObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEmailGetProximalEmailData_Result>().GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetProximalEmailData(eventItem.EventId)).Returns(mockProximalEmailObject.Object);

            Assert.AreEqual(0, ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId).Count());
        }

        [TestMethod()]
        public void EmailNotConfirmed()
        {
            var user = CreateMockUser();
            user.EmailConfirmed = false;
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event to User with Unconfirmed E-mail"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });
            var mockProximalEmail = new List<usp_ZebraEmailGetProximalEmailData_Result> { CreateMockZebraEmailGetProximalEmailDataResult(user) };
            var mockProximalEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetProximalEmailData_Result>>();
            mockProximalEmailObject.Setup(x => x.GetEnumerator()).Returns(mockProximalEmail.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetProximalEmailData(eventItem.EventId)).Returns(mockProximalEmailObject.Object);

            Assert.AreEqual(0, ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId).Count());
        }

        [TestMethod()]
        public void NewCaseNotificationDisabled()
        {
            var user = CreateMockUser();
            user.NewCaseNotificationEnabled = false;
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event to User with Notification Disabled"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });
            var mockProximalEmail = new List<usp_ZebraEmailGetProximalEmailData_Result> { CreateMockZebraEmailGetProximalEmailDataResult(user) };
            var mockProximalEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetProximalEmailData_Result>>();
            mockProximalEmailObject.Setup(x => x.GetEnumerator()).Returns(mockProximalEmail.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetProximalEmailData(eventItem.EventId)).Returns(mockProximalEmailObject.Object);

            Assert.AreEqual(0, ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId).Count());
        }

        [TestMethod()]
        public void ValidProximalEvents()
        {
            var user = CreateMockUser();
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Proximal Test Event"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });
            var mockProximalEmail = new List<usp_ZebraEmailGetProximalEmailData_Result> { CreateMockZebraEmailGetProximalEmailDataResult(user) };
            var mockProximalEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetProximalEmailData_Result>>();
            mockProximalEmailObject.Setup(x => x.GetEnumerator()).Returns(mockProximalEmail.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetProximalEmailData(eventItem.EventId)).Returns(mockProximalEmailObject.Object);
            var mockGeonameIdObject = new Mock<ObjectResult<usp_SearchGeonamesByGeonameIds_Result>>();
            mockGeonameIdObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_SearchGeonamesByGeonameIds_Result>().GetEnumerator());
            mockDbContext.Setup(context => context.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds)).Returns(mockGeonameIdObject.Object);
            var mockDiseaseNameObject = new Mock<ObjectResult<string>>();
            mockDiseaseNameObject.Setup(x => x.GetEnumerator()).Returns(new List<string> { "" }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetDiseaseNameByEventId(eventItem.EventId)).Returns(mockDiseaseNameObject.Object);

            Assert.AreEqual(1, ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId).Count());
        }

        private usp_ZebraEmailGetProximalEmailData_Result CreateMockZebraEmailGetProximalEmailDataResult(ApplicationUser user)
        {
            return new usp_ZebraEmailGetProximalEmailData_Result
            {
                UserId = user.Id,
                Email = user.Email
            };
        }
    }
}