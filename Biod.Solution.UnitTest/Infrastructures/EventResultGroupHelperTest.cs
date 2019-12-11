using System.Collections.Generic;
using System.Linq;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.FilterEventResult;
using Biod.Zebra.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest.Infrastructures
{
    [TestClass]
    public class EventResultGroupHelperTest
    {
        [TestMethod]
        public void TestGroupEvents_DefaultNone_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1 },
                new EventsInfoModel{ EventId = 2 },
                new EventsInfoModel{ EventId = 3 },
                new EventsInfoModel{ EventId = 4 },
                new EventsInfoModel{ EventId = 5 },
                new EventsInfoModel{ EventId = 6 }
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(-1, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(6, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 1, 2, 3, 4, 5, 6 }),
                string.Join(",", groupedEvents.Select(e => e.EventId).ToArray()), 
                "Event order unexpectedly changed during grouping by none");
            Assert.IsFalse(events.Any(e => e.Group != null), "An event has unexpected group when grouping by None");
        }
        
        [TestMethod]
        public void TestGroupEvents_None_Empty()
        {
            var groupedEvents = EventResultGroupHelper.GroupEvents(
                Constants.GroupByFieldTypes.NONE,
                new List<EventsInfoModel>(),
                new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(0, groupedEvents.Count, "Empty events list after grouping no longer empty");
        }
        
        [TestMethod]
        public void TestGroupEvents_None_OneItem()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1 }
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.NONE, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(1, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual(1, groupedEvents.First().EventId, "First event not correct after grouping");
            Assert.IsNull(events.First().Group, "Event has unexpected group when grouping by None");
        }
        
        [TestMethod]
        public void TestGroupEvents_None_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1 },
                new EventsInfoModel{ EventId = 2 },
                new EventsInfoModel{ EventId = 3 },
                new EventsInfoModel{ EventId = 4 },
                new EventsInfoModel{ EventId = 5 },
                new EventsInfoModel{ EventId = 6 }
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.NONE, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(6, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual(
                string.Join(",", new [] { 1, 2, 3, 4, 5, 6 }),
                string.Join(",", groupedEvents.Select(e => e.EventId).ToArray()), 
                "Event order unexpectedly changed during grouping by none");
            Assert.IsFalse(events.Any(e => e.Group != null), "An event has unexpected group when grouping by None");
        }
        
        [TestMethod]
        public void TestGroupEvents_TransmissionMode_Empty()
        {
            var groupedEvents = EventResultGroupHelper.GroupEvents(
                Constants.GroupByFieldTypes.TRANSMISSION_MODE,
                new List<EventsInfoModel>(),
                new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(0, groupedEvents.Count, "Empty events list after grouping no longer empty");
        }
        
        [TestMethod]
        public void TestGroupEvents_TransmissionMode_Null()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Transmissions = null}
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.TRANSMISSION_MODE, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(1, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual(1, groupedEvents.First().EventId, "First event not correct after grouping");
            Assert.AreEqual("1-No Information", events.First().Group, "Event has unexpected group when grouping by Transmission Mode");
        }
        
        [TestMethod]
        public void TestGroupEvents_TransmissionMode_Single()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Transmissions = "Mosquito"},
                new EventsInfoModel{ EventId = 2, Transmissions = "Airborne"}
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.TRANSMISSION_MODE, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(2, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual("0-Mosquito", groupedEvents.First(e => e.EventId == 1).Group, "Event has unexpected group when grouping by Transmission Mode");
            Assert.AreEqual("0-Airborne", groupedEvents.First(e => e.EventId == 2).Group, "Event has unexpected group when grouping by Transmission Mode");
        }
        
        [TestMethod]
        public void TestGroupEvents_TransmissionMode_Multiple()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Transmissions = "Mosquito,Airborne"},
                new EventsInfoModel{ EventId = 2, Transmissions = "Airborne,Feces,Ticks"}
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.TRANSMISSION_MODE, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(5, groupedEvents.Count, "Events list after grouping no longer returning the correct amount of items");
            Assert.AreEqual(2, groupedEvents.Count(e => e.EventId == 1), "Events list after grouping no longer returning the correct amount of items");
            Assert.IsTrue(groupedEvents.Any(e => e.EventId == 1 && e.Group == "0-Mosquito"), "Event is missing a group when grouping by Transmission Mode");
            Assert.IsTrue(groupedEvents.Any(e => e.EventId == 1 && e.Group == "0-Airborne"), "Event is missing a group when grouping by Transmission Mode");
            
            Assert.AreEqual(3, groupedEvents.Count(e => e.EventId == 2), "Events list after grouping no longer returning the correct amount of items");
            Assert.IsTrue(groupedEvents.Any(e => e.EventId == 2 && e.Group == "0-Airborne"), "Event is missing a group when grouping by Transmission Mode");
            Assert.IsTrue(groupedEvents.Any(e => e.EventId == 2 && e.Group == "0-Feces"), "Event is missing a group when grouping by Transmission Mode");
            Assert.IsTrue(groupedEvents.Any(e => e.EventId == 2 && e.Group == "0-Ticks"), "Event is missing a group when grouping by Transmission Mode");
        }
        
        [TestMethod]
        public void TestGroupEvents_BioSecurityRisk_Empty()
        {
            var groupedEvents = EventResultGroupHelper.GroupEvents(
                Constants.GroupByFieldTypes.BIOSECURITY_RISK,
                new List<EventsInfoModel>(),
                new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(0, groupedEvents.Count, "Empty events list after grouping no longer empty");
        }
        
        [TestMethod]
        public void TestGroupEvents_BioSecurityRisk_Null()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, BiosecurityRisk = null}
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.BIOSECURITY_RISK, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(1, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual(1, groupedEvents.First().EventId, "First event not correct after grouping");
            Assert.AreEqual("No/unknown risk", events.First().Group, "Event has unexpected group when grouping by Biosecurity Risk");
        }
        
        [TestMethod]
        public void TestGroupEvents_BioSecurityRisk_No()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, BiosecurityRisk = "No" },
                new EventsInfoModel{ EventId = 2, BiosecurityRisk = "no" }
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.BIOSECURITY_RISK, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(2, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.IsTrue(groupedEvents.All(e => e.Group == "No/unknown risk"), "An event has unexpected group when grouping by Biosecurity Risk");
        }
        
        [TestMethod]
        public void TestGroupEvents_BioSecurityRisk_Category()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, BiosecurityRisk = "A" },
                new EventsInfoModel{ EventId = 2, BiosecurityRisk = "a"  },
                new EventsInfoModel{ EventId = 3, BiosecurityRisk = "B"  },
                new EventsInfoModel{ EventId = 4, BiosecurityRisk = "b"  },
                new EventsInfoModel{ EventId = 5, BiosecurityRisk = "C"  },
                new EventsInfoModel{ EventId = 6, BiosecurityRisk = "c"  }
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.BIOSECURITY_RISK, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(6, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual("Category A", groupedEvents.First(e => e.EventId == 1).Group, "Event has unexpected group when grouping by Biosecurity Risk");
            Assert.AreEqual("Category a", groupedEvents.First(e => e.EventId == 2).Group, "Event has unexpected group when grouping by Biosecurity Risk");
            Assert.AreEqual("Category B", groupedEvents.First(e => e.EventId == 3).Group, "Event has unexpected group when grouping by Biosecurity Risk");
            Assert.AreEqual("Category b", groupedEvents.First(e => e.EventId == 4).Group, "Event has unexpected group when grouping by Biosecurity Risk");
            Assert.AreEqual("Category C", groupedEvents.First(e => e.EventId == 5).Group, "Event has unexpected group when grouping by Biosecurity Risk");
            Assert.AreEqual("Category c", groupedEvents.First(e => e.EventId == 6).Group, "Event has unexpected group when grouping by Biosecurity Risk");
        }
        
        [TestMethod]
        public void TestGroupEvents_PreventionMeasure_Empty()
        {
            var groupedEvents = EventResultGroupHelper.GroupEvents(
                Constants.GroupByFieldTypes.PREVENTION_MEASURE,
                new List<EventsInfoModel>(),
                new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(0, groupedEvents.Count, "Empty events list after grouping no longer empty");
        }
        
        [TestMethod]
        public void TestGroupEvents_PreventionMeasure_Null()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Interventions = null}
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.PREVENTION_MEASURE, events, new List<usp_ZebraDashboardGetInterventionMethods_Result>());
            Assert.AreEqual(1, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual(1, groupedEvents.First().EventId, "First event not correct after grouping");
            Assert.AreEqual(Constants.PreventionTypes.BEHAVIOURAL, events.First().Group, "Event has unexpected group when grouping by Prevention Measure");
        }
        
        [TestMethod]
        public void TestGroupEvents_PreventionMeasure_Unknown()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Interventions = "Isolation"}
            };

            var preventionTypes = new List<usp_ZebraDashboardGetInterventionMethods_Result>
            {
                new usp_ZebraDashboardGetInterventionMethods_Result { InterventionDisplayId = 1, InterventionDisplayName = "Vaccine" }
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.PREVENTION_MEASURE, events, preventionTypes);
            Assert.AreEqual(1, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual(1, groupedEvents.First().EventId, "First event not correct after grouping");
            Assert.AreEqual(Constants.PreventionTypes.BEHAVIOURAL, events.First().Group, "Event has unexpected group when grouping by Prevention Measure");
        }
        
        [TestMethod]
        public void TestGroupEvents_PreventionMeasure_Known()
        {
            var events = new List<EventsInfoModel>
            {
                new EventsInfoModel{ EventId = 1, Interventions = "Isolation"},
                new EventsInfoModel{ EventId = 2, Interventions = "Vaccine"}
            };

            var preventionTypes = new List<usp_ZebraDashboardGetInterventionMethods_Result>
            {
                new usp_ZebraDashboardGetInterventionMethods_Result { InterventionDisplayId = 1, InterventionDisplayName = "Vaccine" },
                new usp_ZebraDashboardGetInterventionMethods_Result { InterventionDisplayId = 2, InterventionDisplayName = "Isolation" }
            };
            
            var groupedEvents = EventResultGroupHelper.GroupEvents(Constants.GroupByFieldTypes.PREVENTION_MEASURE, events, preventionTypes);
            Assert.AreEqual(2, groupedEvents.Count, "Events list after grouping no longer returning the same amount of items");
            Assert.AreEqual("Isolation", groupedEvents.First(e => e.EventId == 1).Group, "Event has unexpected group when grouping by Prevention Measure");
            Assert.AreEqual("Vaccine", groupedEvents.First(e => e.EventId == 2).Group, "Event has unexpected group when grouping by Prevention Measure");
        }
    }
}