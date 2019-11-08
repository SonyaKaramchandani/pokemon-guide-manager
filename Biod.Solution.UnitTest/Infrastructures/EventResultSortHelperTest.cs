using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.FilterEventResult;
using Biod.Zebra.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest.Infrastructures
{
    [TestClass]
    public class EventResultSortHelperTest
    {
        [TestMethod]
        public void TestSortEvents_LastUpdatedDate_Empty()
        {
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.LAST_UPDATED, new List<EventsInfoModel>());
            Assert.AreEqual(0, sortedEvents.Count, "Empty events list after sorting no longer empty");
        }
        
        [TestMethod]
        public void TestSortEvents_LastUpdatedDate_OneItemNull()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, LastUpdatedDate = null }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.LAST_UPDATED, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_LastUpdatedDate_OneItemNonNull()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, LastUpdatedDate = DateTime.Now }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.LAST_UPDATED, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_LastUpdatedDate_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, LastUpdatedDate = DateTime.Now },
                new EventsInfoModel{ EventId = 2, LastUpdatedDate = DateTime.Now.AddDays(3) },
                new EventsInfoModel{ EventId = 3, LastUpdatedDate = DateTime.Now.AddDays(-3) },
                new EventsInfoModel{ EventId = 4, LastUpdatedDate = DateTime.Now.AddYears(-10) },
                new EventsInfoModel{ EventId = 5, LastUpdatedDate = DateTime.Now.AddDays(-50) },
                new EventsInfoModel{ EventId = 6, LastUpdatedDate = DateTime.Now.AddDays(50) }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.LAST_UPDATED, events);
            Assert.AreEqual(6, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 6, 2, 1, 3, 5, 4}),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_StartDate_Empty()
        {
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.EVENT_START_DATE, new List<EventsInfoModel>());
            Assert.AreEqual(0, sortedEvents.Count, "Empty events list after sorting no longer empty");
        }
        
        [TestMethod]
        public void TestSortEvents_StartDate_OneItemNull()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, StartDate = null }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.EVENT_START_DATE, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_StartDate_OneItemNonNull()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, StartDate = DateTime.Now.ToString(CultureInfo.InvariantCulture) }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.EVENT_START_DATE, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_StartDate_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 11, StartDate = DateTime.Now.ToString(CultureInfo.InvariantCulture) },
                new EventsInfoModel{ EventId = 62, StartDate = DateTime.Now.AddDays(3).ToString(CultureInfo.InvariantCulture) },
                new EventsInfoModel{ EventId = 39, StartDate = DateTime.Now.AddDays(-3).ToString(CultureInfo.InvariantCulture) },
                new EventsInfoModel{ EventId = 42, StartDate = DateTime.Now.AddYears(-10).ToString(CultureInfo.InvariantCulture) },
                new EventsInfoModel{ EventId = 25, StartDate = DateTime.Now.AddDays(-50).ToString(CultureInfo.InvariantCulture) },
                new EventsInfoModel{ EventId = 56, StartDate = DateTime.Now.AddDays(50).ToString(CultureInfo.InvariantCulture) }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.EVENT_START_DATE, events);
            Assert.AreEqual(6, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 56, 62, 11, 39, 25, 42}),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_CaseCount_Empty()
        {
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.CASE_COUNT, new List<EventsInfoModel>());
            Assert.AreEqual(0, sortedEvents.Count, "Empty events list after sorting no longer empty");
        }
        
        [TestMethod]
        public void TestSortEvents_CaseCount_OneItemZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, RepCases = 0 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.CASE_COUNT, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_CaseCount_OneItemNonZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, RepCases = 3487 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.CASE_COUNT, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_CaseCount_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 14, RepCases = 1252 },
                new EventsInfoModel{ EventId = 34, RepCases = 0 },
                new EventsInfoModel{ EventId = 39, RepCases = 5817 },
                new EventsInfoModel{ EventId = 78, RepCases = 3 },
                new EventsInfoModel{ EventId = 65, RepCases = 2324 },
                new EventsInfoModel{ EventId = 23, RepCases = 6 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.CASE_COUNT, events);
            Assert.AreEqual(6, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 39, 65, 14, 23, 78, 34 }),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_DeathCount_Empty()
        {
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.DEATH_COUNT, new List<EventsInfoModel>());
            Assert.AreEqual(0, sortedEvents.Count, "Empty events list after sorting no longer empty");
        }
        
        [TestMethod]
        public void TestSortEvents_DeathCount_OneItemZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Deaths = 0 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.DEATH_COUNT, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_DeathCount_OneItemNonZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Deaths = 3487 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.DEATH_COUNT, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_DeathCount_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 14, Deaths = 1252 },
                new EventsInfoModel{ EventId = 34, Deaths = 0 },
                new EventsInfoModel{ EventId = 39, Deaths = 5817 },
                new EventsInfoModel{ EventId = 78, Deaths = 3 },
                new EventsInfoModel{ EventId = 65, Deaths = 2324 },
                new EventsInfoModel{ EventId = 23, Deaths = 6 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.DEATH_COUNT, events);
            Assert.AreEqual(6, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 39, 65, 14, 23, 78, 34 }),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ExportationRisk_Empty()
        {
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_EXPORTATION, new List<EventsInfoModel>());
            Assert.AreEqual(0, sortedEvents.Count, "Empty events list after sorting no longer empty");
        }
        
        [TestMethod]
        public void TestSortEvents_ExportationRisk_OneItemZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, ExportationInfectedTravellersMin = 0 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_EXPORTATION, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ExportationRisk_OneItemNonZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, ExportationInfectedTravellersMin = 0.1238m }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_EXPORTATION, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ExportationRisk_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 434, ExportationInfectedTravellersMin = 0m },
                new EventsInfoModel{ EventId = 352, ExportationInfectedTravellersMin = 0.238m },
                new EventsInfoModel{ EventId = 747, ExportationInfectedTravellersMin = 1m },
                new EventsInfoModel{ EventId = 414, ExportationInfectedTravellersMin = 34.31m },
                new EventsInfoModel{ EventId = 626, ExportationInfectedTravellersMin = 0.000006m },
                new EventsInfoModel{ EventId = 523, ExportationInfectedTravellersMin = 348m }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_EXPORTATION, events);
            Assert.AreEqual(6, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 523, 414, 747, 352, 626, 434 }),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_Empty()
        {
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, new List<EventsInfoModel>());
            Assert.AreEqual(0, sortedEvents.Count, "Empty events list after sorting no longer empty");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_OneItemLocalSpread()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, LocalSpread = true }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_OneItemZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, ImportationProbabilityMax = 0 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_OneItemNonZero()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, ImportationProbabilityMax = 0.1238m }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, events);
            Assert.AreEqual(1, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(1, sortedEvents.First().EventId, "First event not correct after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_MultipleLocalSpread()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 434, LocalSpread = true, RepCases = 7 },
                new EventsInfoModel{ EventId = 352, LocalSpread = true, RepCases = 231 },
                new EventsInfoModel{ EventId = 747, LocalSpread = true, RepCases = 153 },
                new EventsInfoModel{ EventId = 414, LocalSpread = true, RepCases = 0 },
                new EventsInfoModel{ EventId = 626, LocalSpread = true, RepCases = 1 },
                new EventsInfoModel{ EventId = 523, LocalSpread = true, RepCases = 10 }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, events);
            Assert.AreEqual(6, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 352, 747, 523, 434, 626, 414 }),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_MultipleLocalSpreadEqualCases()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 434, LocalSpread = true, RepCases = 240, EventTitle = "Ak" },
                new EventsInfoModel{ EventId = 352, LocalSpread = true, RepCases = 240, EventTitle = "Ad" },
                new EventsInfoModel{ EventId = 5156, LocalSpread = true, RepCases = 13, EventTitle = "Zx" },
                new EventsInfoModel{ EventId = 626, LocalSpread = true, RepCases = 240, EventTitle = "B" },
                new EventsInfoModel{ EventId = 523, LocalSpread = true, RepCases = 240, EventTitle = "Le" },
                new EventsInfoModel{ EventId = 437, LocalSpread = true, RepCases = 13, EventTitle = "Le" },
                new EventsInfoModel{ EventId = 4331, LocalSpread = true, RepCases = 13, EventTitle = "Ad" },
                new EventsInfoModel{ EventId = 747, LocalSpread = true, RepCases = 240, EventTitle = "La" },
                new EventsInfoModel{ EventId = 414, LocalSpread = true, RepCases = 240, EventTitle = "D" },
                new EventsInfoModel{ EventId = 141, LocalSpread = true, RepCases = 55, EventTitle = "Aa" }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, events);
            Assert.AreEqual(10, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 352, 434, 626, 414, 747, 523, 141, 4331, 437, 5156 }),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_MultipleNonLocalSpread()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 434, ImportationProbabilityMax = 0m },
                new EventsInfoModel{ EventId = 352, ImportationProbabilityMax = 0.238m },
                new EventsInfoModel{ EventId = 747, ImportationProbabilityMax = 1m },
                new EventsInfoModel{ EventId = 414, ImportationProbabilityMax = 34.31m },
                new EventsInfoModel{ EventId = 626, ImportationProbabilityMax = 0.000006m },
                new EventsInfoModel{ EventId = 523, ImportationProbabilityMax = 348m }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, events);
            Assert.AreEqual(6, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 523, 414, 747, 352, 626, 434 }),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
        
        [TestMethod]
        public void TestSortEvents_ImportationRisk_MultipleMixed()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 54854, LocalSpread = true, RepCases = 7 },
                new EventsInfoModel{ EventId = 1231, LocalSpread = true, RepCases = 231 },
                new EventsInfoModel{ EventId = 747, ImportationProbabilityMax = 1m },
                new EventsInfoModel{ EventId = 2141, ImportationProbabilityMax = 34.31m },
                new EventsInfoModel{ EventId = 3546, ImportationProbabilityMax = 0.000006m },
                new EventsInfoModel{ EventId = 523, ImportationProbabilityMax = 348m },
                new EventsInfoModel{ EventId = 243, LocalSpread = true, RepCases = 996, EventTitle = "Ab"},
                new EventsInfoModel{ EventId = 4234, LocalSpread = true, RepCases = 996, EventTitle = "xx" },
                new EventsInfoModel{ EventId = 3453, LocalSpread = true, RepCases = 996, EventTitle = "Ed" },
                new EventsInfoModel{ EventId = 23727, LocalSpread = true, RepCases = 15 },
                new EventsInfoModel{ EventId = 2352, LocalSpread = true, RepCases = 214, EventTitle = "Aa" },
                new EventsInfoModel{ EventId = 547, LocalSpread = true, RepCases = 214, EventTitle = "Cd" },
                new EventsInfoModel{ EventId = 56854, ImportationProbabilityMax = 0.0003426m },
                new EventsInfoModel{ EventId = 141, ImportationProbabilityMax = 1.000006m }
            };
            
            var sortedEvents = EventResultSortHelper.SortEvents(Constants.OrderByFieldTypes.RISK_OF_IMPORTATION, events);
            Assert.AreEqual(14, sortedEvents.Count, "Events list after sorting no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 243, 3453, 4234, 1231, 2352, 547, 23727, 54854, 523, 2141, 141, 747, 56854, 3546 }),
                string.Join(",", sortedEvents.Select(e => e.EventId).ToArray()), 
                "Events not in correct order after sorting");
        }
    }
}