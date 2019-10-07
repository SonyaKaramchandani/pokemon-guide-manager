using Biod.Zebra.Library.EntityModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Biod.Zebra.Library.Models.Notification;
using Microsoft.AspNet.Identity;
using Biod.Zebra.Library.Models;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Biod.Solution.UnitTest.Api.Surveillance.ZebraNotificationsTest
{
    /// <summary>
    /// Tests lists of <c>NotificationViewModel</c> obtained from <c>WeeklyViewModel.GetNotificationViewModelList</c>
    /// </summary>
    [TestClass]
    public class WeeklyViewModelTest : NotificationTest
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
        public void EmptyWeeklyEvents()
        {
            var mockProximalEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetWeeklyEmailData_Result>>();
            mockProximalEmailObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEmailGetWeeklyEmailData_Result>().GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetWeeklyEmailData()).Returns(mockProximalEmailObject.Object);

            Assert.AreEqual(0, WeeklyViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object).Count());
        }

        [TestMethod()]
        public void EmailNotConfirmed()
        {
            var user = CreateMockUser();
            user.EmailConfirmed = false;
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var mockWeeklyEmail = new List<usp_ZebraEmailGetWeeklyEmailData_Result> { CreateMockZebraEmailGetWeeklyEmailDataResult(user) };
            var mockWeeklyEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetWeeklyEmailData_Result>>();
            mockWeeklyEmailObject.Setup(x => x.GetEnumerator()).Returns(mockWeeklyEmail.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetWeeklyEmailData()).Returns(mockWeeklyEmailObject.Object);

            Assert.AreEqual(0, WeeklyViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object).Count());
        }

        [TestMethod()]
        public void WeeklyNotificationDisabled()
        {
            var user = CreateMockUser();
            user.WeeklyOutbreakNotificationEnabled = false;
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var mockWeeklyEmail = new List<usp_ZebraEmailGetWeeklyEmailData_Result> { CreateMockZebraEmailGetWeeklyEmailDataResult(user) };
            var mockWeeklyEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetWeeklyEmailData_Result>>();
            mockWeeklyEmailObject.Setup(x => x.GetEnumerator()).Returns(mockWeeklyEmail.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetWeeklyEmailData()).Returns(mockWeeklyEmailObject.Object);

            Assert.AreEqual(0, WeeklyViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object).Count());
        }

        [TestMethod()]
        public void ValidWeeklyEvents()
        {
            var user = CreateMockUser();
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var mockWeeklyEmail = new List<usp_ZebraEmailGetWeeklyEmailData_Result> { CreateMockZebraEmailGetWeeklyEmailDataResult(user) };
            var mockWeeklyEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetWeeklyEmailData_Result>>();
            mockWeeklyEmailObject.Setup(x => x.GetEnumerator()).Returns(mockWeeklyEmail.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetWeeklyEmailData()).Returns(mockWeeklyEmailObject.Object);

            Assert.AreEqual(1, WeeklyViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object).Count());
        }

        private usp_ZebraEmailGetWeeklyEmailData_Result CreateMockZebraEmailGetWeeklyEmailDataResult(ApplicationUser user)
        {
            return new usp_ZebraEmailGetWeeklyEmailData_Result
            {
                UserId = user.Id,
                Email = user.Email,
                LocalSpread = 1
            };
        }
    }
}
