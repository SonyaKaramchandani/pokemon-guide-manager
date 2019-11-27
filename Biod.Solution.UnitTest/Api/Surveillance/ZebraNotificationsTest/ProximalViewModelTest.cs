using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Notification;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Constants = Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Solution.UnitTest.Api.Surveillance.ZebraNotificationsTest
{
    /// <summary>
    /// Tests lists of <c>NotificationViewModel</c> obtained from <c>ProximalVliewModel.GetNotificationViewModelList</c>
    /// </summary>
    [TestClass()]
    public class ProximalViewModelTest : NotificationTest
    {
        ProximalMockDbSet mockDbSet;

        [TestInitialize()]
        public override void Initialize()
        {
            base.Initialize();
            mockDbSet = new ProximalMockDbSet(mockDbContext);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object);
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { }.AsQueryable());
        }

        [TestMethod()]
        public void NoProximalUsers()
        {
            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event with No Proximal Users"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });
            var mockProximalUsers = new Mock<ObjectResult<usp_ZebraEventGetProximalUsersByEventId_Result>>();
            mockProximalUsers.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEventGetProximalUsersByEventId_Result>().GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetProximalUsersByEventId(eventItem.EventId)).Returns(mockProximalUsers.Object);

            Assert.AreEqual(0, ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId).Count());
        }

        [TestMethod()]
        public void EventHasNoPositiveDelta()
        {
            var user = CreateMockUser();
            var geonameId = random.Next();
            user.AoiGeonameIds = $"{geonameId}";
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Test Event with No Positive Delta"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });
            var mockProximalUsers = new Mock<ObjectResult<usp_ZebraEventGetProximalUsersByEventId_Result>>();

            mockProximalUsers.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEventGetProximalUsersByEventId_Result>
            {
                new usp_ZebraEventGetProximalUsersByEventId_Result { UserId = user.Id, GeonameId = geonameId }
            }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetProximalUsersByEventId(eventItem.EventId)).Returns(mockProximalUsers.Object);
            mockDbSet.AddEventToEventLocationTables(eventItem.EventId, geonameId, Constants.LocationType.CITY, false);

            Assert.AreEqual(0, ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId).Count());
        }

        [TestMethod()]
        public void EventHasPositiveDelta()
        {
            var user = CreateMockUser();
            var geonameId = random.Next();
            user.AoiGeonameIds = $"{geonameId}";
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Proximal Test Event with Positive Delta"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });
            var mockProximalUsers = new Mock<ObjectResult<usp_ZebraEventGetProximalUsersByEventId_Result>>();

            mockProximalUsers.Setup(x => x.GetEnumerator()).Returns(new List<usp_ZebraEventGetProximalUsersByEventId_Result>
            {
                new usp_ZebraEventGetProximalUsersByEventId_Result { UserId = user.Id, GeonameId = geonameId }
            }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetProximalUsersByEventId(eventItem.EventId)).Returns(mockProximalUsers.Object);
            mockDbSet.AddEventToEventLocationTables(eventItem.EventId, geonameId, Constants.LocationType.CITY, true);

            var mockGeonameIdObject = new Mock<ObjectResult<usp_SearchGeonamesByGeonameIds_Result>>();
            mockGeonameIdObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_SearchGeonamesByGeonameIds_Result>
            {
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId, DisplayName = "", LocationType = Constants.LocationTypeDescription.CITY }
            }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds)).Returns(mockGeonameIdObject.Object);
            var mockDiseaseNameObject = new Mock<ObjectResult<string>>();
            mockDiseaseNameObject.Setup(x => x.GetEnumerator()).Returns(new List<string> { "Disease Name" }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetDiseaseNameByEventId(eventItem.EventId)).Returns(mockDiseaseNameObject.Object);

            var notifications = ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId);
            Assert.AreEqual(1, notifications.Count());
            Assert.AreEqual(geonameId, ((ProximalViewModel)notifications[0]).EventGeonameId);
            Assert.AreEqual(user.Id, ((ProximalViewModel)notifications[0]).UserId);
        }

        [TestMethod()]
        public void UserHasMultipleAoi()
        {
            var user = CreateMockUser();
            var geonameId1 = random.Next();
            var geonameId2 = random.Next();
            var geonameId3 = random.Next();
            user.AoiGeonameIds = $"{geonameId1},{geonameId2},{geonameId3}";
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Proximal Test Event Affecting Multiple Locations"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });

            var mockProximalUsers = new List<usp_ZebraEventGetProximalUsersByEventId_Result>
            {
                new usp_ZebraEventGetProximalUsersByEventId_Result { UserId = user.Id, GeonameId = geonameId1 },
                new usp_ZebraEventGetProximalUsersByEventId_Result { UserId = user.Id, GeonameId = geonameId2 }
            };
            var mockProximalUsersObject = new Mock<ObjectResult<usp_ZebraEventGetProximalUsersByEventId_Result>>();
            mockProximalUsersObject.Setup(x => x.GetEnumerator()).Returns(mockProximalUsers.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetProximalUsersByEventId(eventItem.EventId)).Returns(mockProximalUsersObject.Object);
            mockDbSet.AddEventToEventLocationTables(eventItem.EventId, geonameId1, Constants.LocationType.CITY, true);
            mockDbSet.AddEventToEventLocationTables(eventItem.EventId, geonameId2, Constants.LocationType.PROVINCE, true);

            var mockGeonameIdObject = new Mock<ObjectResult<usp_SearchGeonamesByGeonameIds_Result>>();
            mockGeonameIdObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_SearchGeonamesByGeonameIds_Result>
            {
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId1, DisplayName = "", LocationType = Constants.LocationTypeDescription.CITY },
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId2, DisplayName = "", LocationType = Constants.LocationTypeDescription.PROVINCE },
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId3, DisplayName = "", LocationType = Constants.LocationTypeDescription.CITY }
            }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_SearchGeonamesByGeonameIds(user.AoiGeonameIds)).Returns(mockGeonameIdObject.Object);
            var mockDiseaseNameObject = new Mock<ObjectResult<string>>();
            mockDiseaseNameObject.Setup(x => x.GetEnumerator()).Returns(new List<string> { "Disease Name" }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetDiseaseNameByEventId(eventItem.EventId)).Returns(mockDiseaseNameObject.Object);

            var notifications = ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId);
            Assert.AreEqual(2, notifications.Count());
            ValidateProximalNotificationList(notifications, mockProximalUsers);
        }

        [TestMethod()]
        public void MultipleUsersWithDifferentAoi()
        {
            var user1 = CreateMockUser();
            var user2 = CreateMockUser();
            var geonameId1 = random.Next();
            var geonameId2 = random.Next();
            var geonameId3 = random.Next();
            user1.AoiGeonameIds = $"{geonameId1},{geonameId2}";
            user2.AoiGeonameIds = $"{geonameId2},{geonameId3}";
            mockUserManager.Setup(mgr => mgr.Users).Returns(new List<ApplicationUser> { user1, user2 }.AsQueryable());

            var eventItem = new Event
            {
                DiseaseId = random.Next(100),
                EventId = random.Next(1000, 9999),
                EventTitle = "Proximal Test Event Affecting Multiple Locations"
            };
            mockDbContext.Setup(context => context.Events).ReturnsDbSet(new List<Event> { eventItem });

            var mockProximalUsers = new List<usp_ZebraEventGetProximalUsersByEventId_Result>
            {
                new usp_ZebraEventGetProximalUsersByEventId_Result { UserId = user1.Id, GeonameId = geonameId1 },
                new usp_ZebraEventGetProximalUsersByEventId_Result { UserId = user1.Id, GeonameId = geonameId2 },
                new usp_ZebraEventGetProximalUsersByEventId_Result { UserId = user2.Id, GeonameId = geonameId2 }
            };
            var mockProximalUsersObject = new Mock<ObjectResult<usp_ZebraEventGetProximalUsersByEventId_Result>>();
            mockProximalUsersObject.Setup(x => x.GetEnumerator()).Returns(mockProximalUsers.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetProximalUsersByEventId(eventItem.EventId)).Returns(mockProximalUsersObject.Object);
            mockDbSet.AddEventToEventLocationTables(eventItem.EventId, geonameId1, Constants.LocationType.CITY, true);
            mockDbSet.AddEventToEventLocationTables(eventItem.EventId, geonameId2, Constants.LocationType.PROVINCE, true);

            var mockUser1GeonameIdObject = new Mock<ObjectResult<usp_SearchGeonamesByGeonameIds_Result>>();
            mockUser1GeonameIdObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_SearchGeonamesByGeonameIds_Result>
            {
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId1, DisplayName = "", LocationType = Constants.LocationTypeDescription.CITY },
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId2, DisplayName = "", LocationType = Constants.LocationTypeDescription.PROVINCE }
            }.GetEnumerator());
            var mockUser2GeonameIdObject = new Mock<ObjectResult<usp_SearchGeonamesByGeonameIds_Result>>();
            mockUser2GeonameIdObject.Setup(x => x.GetEnumerator()).Returns(new List<usp_SearchGeonamesByGeonameIds_Result>
            {
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId2, DisplayName = "", LocationType = Constants.LocationTypeDescription.PROVINCE },
                new usp_SearchGeonamesByGeonameIds_Result { GeonameId = geonameId3, DisplayName = "", LocationType = Constants.LocationTypeDescription.CITY }
            }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_SearchGeonamesByGeonameIds(user1.AoiGeonameIds)).Returns(mockUser1GeonameIdObject.Object);
            mockDbContext.Setup(context => context.usp_SearchGeonamesByGeonameIds(user2.AoiGeonameIds)).Returns(mockUser2GeonameIdObject.Object);
            var mockDiseaseNameObject = new Mock<ObjectResult<string>>();
            mockDiseaseNameObject.Setup(x => x.GetEnumerator()).Returns(new List<string> { "Disease Name" }.GetEnumerator());
            mockDbContext.Setup(context => context.usp_ZebraEventGetDiseaseNameByEventId(eventItem.EventId)).Returns(mockDiseaseNameObject.Object);

            var notifications = ProximalViewModel.GetNotificationViewModelList(mockDbContext.Object, mockUserManager.Object, eventItem.EventId);
            Assert.AreEqual(3, notifications.Count());
            ValidateProximalNotificationList(notifications, mockProximalUsers);
        }

        private void ValidateProximalNotificationList(List<NotificationViewModel> notifications, List<usp_ZebraEventGetProximalUsersByEventId_Result> proximalUsers)
        {
            var usersById = proximalUsers.GroupBy(u => u.UserId)
                .ToDictionary(g => g.Key, g => new HashSet<int>(g.Where(r => r.GeonameId != null).Select(r => (int)r.GeonameId)));
            Assert.IsTrue(notifications
                .GroupBy(n => n.UserId)
                .All(g =>
                {
                    if (!usersById.ContainsKey(g.Key) || g.Count() != usersById[g.Key].Count())
                    {
                        return false;
                    }
                    foreach (ProximalViewModel model in g)
                    {
                        if (!usersById[model.UserId].Contains(model.EventGeonameId))
                        {
                            return false;
                        }
                    }

                    return true;
                }));
        }
    }
}