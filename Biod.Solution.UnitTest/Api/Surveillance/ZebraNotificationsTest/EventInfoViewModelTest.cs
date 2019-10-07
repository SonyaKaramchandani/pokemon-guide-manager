using Biod.Zebra.Library.EntityModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Biod.Zebra.Library.Models.Notification;
using Microsoft.AspNet.Identity;
using Biod.Zebra.Library.Models;
using System.Data.Entity.Core.Objects;
using System;
using System.Configuration;
using System.Linq;
using static Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Solution.UnitTest.Api.Surveillance.ZebraNotificationsTest
{
    /// <summary>
    /// Tests lists of <c>NotificationViewModel</c> obtained from <c>EventInfoViewModel.GetNotificationViewModelList</c>
    /// </summary>
    [TestClass()]
    public class EventInfoViewModelTest : NotificationTest
    {
        private static readonly int DISTANCE_BUFFER = 100000;

        [TestInitialize()]
        public override void Initialize()
        {
            base.Initialize();

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object);
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { CreateMockUser() }.AsQueryable());

            ConfigurationManager.AppSettings["EventDistanceBuffer"] = DISTANCE_BUFFER.ToString();
        }

        [TestMethod()]
        public void EmptyLocalEvents()
        {
            var eventId = random.Next(100, 9999);

            var mockResult = new Mock<ObjectResult<usp_ZebraEmailGetEventByEventId_Result>>();
            mockResult.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEmailGetEventByEventId_Result>().GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetEventByEventId(eventId, DISTANCE_BUFFER)).Returns(mockResult.Object);

            Assert.AreEqual(0, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventId, true).Count());
        }

        [TestMethod()]
        public void EmptyNonLocalEvents()
        {
            var eventId = random.Next(100, 9999);

            var mockResult = new Mock<ObjectResult<usp_ZebraEmailGetEventByEventId_Result>>();
            mockResult.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEmailGetEventByEventId_Result>().GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetEventByEventId(eventId, DISTANCE_BUFFER)).Returns(mockResult.Object);

            Assert.AreEqual(0, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventId, false).Count());
        }

        [TestMethod()]
        public void LocalEventAlreadySent()
        {
            var user = CreateMockUser();
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event Already Sent"
            };
            var eventEmail = CreateMockZebraEmailGetEventByEventIdResult(user, true, eventItem);
            var events = new List<usp_ZebraEmailGetEventByEventId_Result> { eventEmail };
            SetupSpMocks(events, eventItem.EventId, eventEmail.AoiGeonameIds, user, true);

            Assert.AreEqual(0, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId, true).Count());
        }

        [TestMethod()]
        public void NonLocalEventAlreadySent()
        {
            var user = CreateMockUser();
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event To Be Sent"
            };
            var eventEmail = CreateMockZebraEmailGetEventByEventIdResult(user, true, eventItem);
            var events = new List<usp_ZebraEmailGetEventByEventId_Result> { eventEmail };
            SetupSpMocks(events, eventItem.EventId, eventEmail.AoiGeonameIds, user, true);

            Assert.AreEqual(0, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId, false).Count());
        }

        [TestMethod()]
        public void LocalEventEmailNotConfirmed()
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
            var eventEmail = CreateMockZebraEmailGetEventByEventIdResult(user, true, eventItem);
            var events = new List<usp_ZebraEmailGetEventByEventId_Result> { eventEmail };
            SetupSpMocks(events, eventItem.EventId, eventEmail.AoiGeonameIds, user, true);

            Assert.AreEqual(0, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId, true).Count());
        }

        [TestMethod()]
        public void NonLocalEventEmailNotConfirmed()
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
            var eventEmail = CreateMockZebraEmailGetEventByEventIdResult(user, true, eventItem);
            var events = new List<usp_ZebraEmailGetEventByEventId_Result> { eventEmail };
            SetupSpMocks(events, eventItem.EventId, eventEmail.AoiGeonameIds, user, true);

            Assert.AreEqual(0, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId, false).Count());
        }

        [TestMethod()]
        public void LocalEventNotYetSent()
        {
            var user = CreateMockUser();
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event Already Sent"
            };
            var eventEmail = CreateMockZebraEmailGetEventByEventIdResult(user, true, eventItem);
            var events = new List<usp_ZebraEmailGetEventByEventId_Result> { eventEmail };
            SetupSpMocks(events, eventItem.EventId, eventEmail.AoiGeonameIds, user, false);

            Assert.AreEqual(1, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId, true).Count());
        }

        [TestMethod()]
        public void NonLocalEventNotYetSent()
        {
            var user = CreateMockUser();
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event To Be Sent"
            };
            var eventEmail = CreateMockZebraEmailGetEventByEventIdResult(user, false, eventItem);
            var events = new List<usp_ZebraEmailGetEventByEventId_Result> { eventEmail };
            SetupSpMocks(events, eventItem.EventId, eventEmail.AoiGeonameIds, user, false);

            Assert.AreEqual(1, EventInfoViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId, false).Count());
        }

        private void SetupSpMocks(List<usp_ZebraEmailGetEventByEventId_Result> events, int eventId, string aoiGeonameIds, ApplicationUser user, bool isSent)
        {
            var mockIsSentObject = new Mock<ObjectResult<bool?>>();
            mockIsSentObject.Setup(x => x.GetEnumerator()).Returns(new List<bool?> { isSent }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetIsEmailSent(eventId, EmailTypes.EVENT_EMAIL, user.Email, user.AoiGeonameIds)).Returns(mockIsSentObject.Object);

            var mockEventEmailObject = new Mock<ObjectResult<usp_ZebraEmailGetEventByEventId_Result>>();
            mockEventEmailObject.Setup(x => x.GetEnumerator()).Returns(events.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetEventByEventId(eventId, DISTANCE_BUFFER)).Returns(mockEventEmailObject.Object);

            var mockAirports = new List<usp_ZebraEmailGetEventAirportInfo_Result> { CreateAirportInfo() };
            var mockAirportsObject = new Mock<ObjectResult<usp_ZebraEmailGetEventAirportInfo_Result>>();
            mockAirportsObject.Setup(x => x.GetEnumerator()).Returns(mockAirports.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEmailGetEventAirportInfo(eventId, user.Id)).Returns(mockAirportsObject.Object);

            var mockImportationRisk = new Mock<usp_ZebraEventGetImportationRisk_Result>().Object;
            var mockImportationRiskObject = new Mock<ObjectResult<usp_ZebraEventGetImportationRisk_Result>>();
            mockImportationRiskObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEventGetImportationRisk_Result> { mockImportationRisk }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetImportationRisk(eventId, aoiGeonameIds)).Returns(mockImportationRiskObject.Object);

            var mockLocationShapes = new Mock<usp_ZebraPlaceGetLocationShapeByGeonameId_Result>().Object;
            var mockLocationShapesObject = new Mock<ObjectResult<usp_ZebraPlaceGetLocationShapeByGeonameId_Result>>();
            mockLocationShapesObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraPlaceGetLocationShapeByGeonameId_Result> { mockLocationShapes }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraPlaceGetLocationShapeByGeonameId(aoiGeonameIds)).Returns(mockLocationShapesObject.Object);
        }

        private usp_ZebraEmailGetEventByEventId_Result CreateMockZebraEmailGetEventByEventIdResult(ApplicationUser user, bool isLocal, Event eventItem)
        {
            return new usp_ZebraEmailGetEventByEventId_Result
            {
                AoiGeonameIds = user.AoiGeonameIds,
                Brief = "An outbreak of measles has been occurring in England.  Measles has an incubation period of 7-21 days. The most common symptoms are cough, coryza, conjunctiva, and an erythematous, maculopapular, blanching rash.",
                DiseaseId = (int)eventItem.DiseaseId,
                DoNotTrackEnabled = user.DoNotTrackEnabled,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                EndDate = new DateTime(),
                EventId = eventItem.EventId,
                EventTitle = eventItem.EventTitle,
                IsLocal = isLocal ? 1 : 0,
                IsPaidUser = true,
                LastUpdatedDate = DateTime.Now,
                OutbreakPotentialAttributeId = 1,
                StartDate = DateTime.Now.AddDays(-15),
                UserId = user.Id,
            };
        }

        private usp_ZebraEmailGetEventAirportInfo_Result CreateAirportInfo()
        {
            return new usp_ZebraEmailGetEventAirportInfo_Result
            {
                AirportName = GenerateRandomString(10),
                AirportCode = GenerateRandomString(3),
                ProbabilityMin = 0,
                ProbabilityMax = 0,
                InfectedTravellersMin = 0,
                InfectedTravellersMax = 0
            };
        }
    }
}