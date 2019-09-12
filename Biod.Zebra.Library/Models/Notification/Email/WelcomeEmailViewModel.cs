using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Biod.Zebra.Library.Models.Notification.Email
{
    public class WelcomeEmailViewModel : EmailViewModel
    {
        private readonly int _numOfEventsToShow = Convert.ToInt16(ConfigurationManager.AppSettings.Get("ZebraWelcomeEmailNumOfEventsToShow"));

        public override string ViewFilePath => "~/Views/Home/WelcomeEmail.cshtml";

        public override int NotificationType => Constants.EmailTypes.WELCOME_EMAIL;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserLocationName { get; set; }

        public List<WelcomeEmailEventViewModel> LocalEvents { get; set; }

        public int AdditionalLocalCount { get; set; }

        public List<WelcomeEmailEventViewModel> NonLocalEvents { get; set; }

        public int AdditionalNonLocalCount { get; set; }

        public WelcomeEmailViewModel Compute(BiodZebraEntities dbContext)
        {
            var result = dbContext.usp_ZebraEventGetEventSummary(UserId, AoiGeonameIds, "", "", "", "", "", true).AsQueryable();
            var eventsGroupedByLocal = result.GroupBy(e => e.LocalSpread).ToArray();

            var outbreakPotentialCategories = dbContext.usp_ZebraDashboardGetOutbreakPotentialCategories().ToList();

            var localEvents = eventsGroupedByLocal.FirstOrDefault(g => g.Key == 1)?.ToArray() ?? new usp_ZebraEventGetEventSummary_Result[0];
            var nonLocalEvents = eventsGroupedByLocal.FirstOrDefault(g => g.Key == 0)?.ToArray() ?? new usp_ZebraEventGetEventSummary_Result[0];

            AdditionalLocalCount = Math.Max(localEvents.Count() - _numOfEventsToShow, 0);
            LocalEvents = localEvents
                .OrderByDescending(e => e.RepCases ?? 0)
                .ThenBy(e => e.EventTitle)
                .Take(_numOfEventsToShow)
                .Select(e => new WelcomeEmailEventViewModel()
                {
                    EventId = e.EventId ?? 0,
                    EventTitle = e.EventTitle
                })
                .ToList();

            AdditionalNonLocalCount = Math.Max(nonLocalEvents.Count() - _numOfEventsToShow, 0);
            NonLocalEvents = nonLocalEvents
                .OrderByDescending(e => e.ImportationInfectedTravellersMin ?? 0)
                .ThenByDescending(e => e.ImportationMinProbability ?? 0)
                .Take(_numOfEventsToShow)
                .Select(e => new WelcomeEmailEventViewModel()
                {
                    EventId = e.EventId ?? 0,
                    EventTitle = e.EventTitle,
                    MinVolume = e.ImportationInfectedTravellersMin ?? 0,
                    MaxVolume = e.ImportationInfectedTravellersMax ?? 0,
                    MinProbability = e.ImportationMinProbability ?? 0,
                    MaxProbability = e.ImportationMaxProbability ?? 0,
                    RiskLabel = GetRiskLabel(OutbreakPotentialCategoryModel.GetOutbreakPotentialCategory(
                        dbContext, 
                        e.EventId ?? 0, 
                        e.DiseaseId ?? 0, 
                        e.OutbreakPotentialAttributeId ?? 5, // Default to Unknown due to DS not always providing attribute ID
                        outbreakPotentialCategories,
                        AoiGeonameIds))
                })
                .ToList();

            return this;
        }

        private string GetRiskLabel(List<EventLocationsOutbreakPotentialModel> outbreakPotentialModels)
        {
            EventLocationsOutbreakPotentialModel model = outbreakPotentialModels.FirstOrDefault();
            if (model == null)
            {
                return "";
            }

            if (model.AttributeId == 1 || model.AttributeId == 3 && model.MapThreshold == ">0")
            {
                return "Sustained local transmission possible";
            }
            if (model.AttributeId == 2)
            {
                return "Sporadic local transmission possible";
            }
            if (model.AttributeId == 4 || model.AttributeId == 3 && model.MapThreshold == "=0")
            {
                return "May be seen in returning travellers";
            }
            if (model.AttributeId == 5)
            {
                return "Local transmissibility unknown";
            }
            return "";
        }

        public class WelcomeEmailEventViewModel
        {
            public int EventId { get; set; }

            public string EventTitle { get; set; }

            public decimal MinProbability { get; set; }

            public decimal MaxProbability { get; set; }

            public decimal MinVolume { get; set; }

            public decimal MaxVolume { get; set; }

            public string RiskLabel { get; set; }
        }
    }
}