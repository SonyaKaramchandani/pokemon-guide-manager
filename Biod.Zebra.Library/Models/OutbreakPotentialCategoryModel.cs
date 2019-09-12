using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Biod.Zebra.Library.Models
{
    public static class OutbreakPotentialCategoryModel
    {
        public static List<EventLocationsOutbreakPotentialModel> GetOutbreakPotentialCategory(
            BiodZebraEntities zebraDbContext,
            int eventId,
            int diseaseId,
            int outbreakPotentialAttributeId,
            List<usp_ZebraDashboardGetOutbreakPotentialCategories_Result> outbreakPotentialCategories,
            string aoiGeonameIds = "")
        {
            List<EventLocationsOutbreakPotentialModel> eventLocationsOutbreakPotentialModels = new List<EventLocationsOutbreakPotentialModel>();
            outbreakPotentialCategories = outbreakPotentialCategories.Where(x => x.AttributeId == outbreakPotentialAttributeId).ToList();
            var outbreakPotentialCategory = outbreakPotentialCategories.FirstOrDefault();
            //if the AttributeId = 3 which is duplicated and the NeedsMap is true which means we need to check for the George RiskValue 
            if (outbreakPotentialCategory != null && ((outbreakPotentialCategories.Count() > 1) && (outbreakPotentialCategory.NeedsMap)))
            {
                //Outbreak potentials needs map. Needs to call George risk API to get the map threshhold
                //the effective message is decided according to the map threshhold (">0" or "=0")
                //If isForEmail is true, get the user location information for email alert EventEmail.cshtml
                //Else if isForEmail is false, get the event locations
                dynamic locationsShapes = zebraDbContext.usp_ZebraPlaceGetLocationShapeByGeonameId(aoiGeonameIds).ToList();

                if (locationsShapes == null) return eventLocationsOutbreakPotentialModels;
                foreach (var locationShape in locationsShapes)
                {
                    string jsonStringResultAsyncString;
                    if (locationShape.LocationType.ToLower() == "city")
                    {
                        string[] longlat = locationShape.ShapeAsText.Replace("POINT (", "").Replace(")", "").Split(' ');
                        jsonStringResultAsyncString = JsonStringResultClass.GetJsonStringResultAsync(
                            ConfigurationManager.AppSettings.Get("GeorgeApiBaseUrl"),
                            "/location/risks?latitude=" + longlat[1] + "&longitude=" + longlat[0],
                            ConfigurationManager.AppSettings.Get(@"GeorgeApiUserName"),
                            ConfigurationManager.AppSettings.Get("GeorgeApiPassword")).Result;
                    }
                    else
                    {
                        //if the location type is province and countries
                        jsonStringResultAsyncString = JsonStringResultClass.GetJsonStringResultAsync(
                            ConfigurationManager.AppSettings.Get("GeorgeApiBaseUrl"),
                            "/location/risksbygeonameId?geonameId=" + locationShape.GeonameId,
                            ConfigurationManager.AppSettings.Get(@"GeorgeApiUserName"),
                            ConfigurationManager.AppSettings.Get("GeorgeApiPassword")).Result;
                    }
                    var georgeRisks = JsonConvert.DeserializeObject<GeorgeRiskClass>(jsonStringResultAsyncString);
                    var diseaseRiskLocations = georgeRisks.locations.ToList();
                    var diseaseRisks = diseaseRiskLocations.FirstOrDefault()?.diseaseRisks;
                    var diseaseRisk = (diseaseRisks ?? throw new InvalidOperationException()).Where(x => x.diseaseId == diseaseId).ToList();

                    if (diseaseRisk.Any() && diseaseRisk.FirstOrDefault().defaultRisk.riskValue > 0)
                    {
                        outbreakPotentialCategory = outbreakPotentialCategories.FirstOrDefault(x => x.AttributeId == outbreakPotentialAttributeId &&
                                                                                                    x.NeedsMap && x.MapThreshold == ">0");
                    }
                    //if no diseases are matching the event disease then take the default outbreak potential
                    else
                    {
                        outbreakPotentialCategory = outbreakPotentialCategories.FirstOrDefault(x => x.AttributeId == outbreakPotentialAttributeId &&
                                                                                                    x.NeedsMap && x.MapThreshold == "=0");
                    }

                    if (outbreakPotentialCategory != null)
                        eventLocationsOutbreakPotentialModels.Add(new EventLocationsOutbreakPotentialModel
                        {
                            AttributeId = outbreakPotentialCategory.AttributeId,
                            EffectiveMessage = outbreakPotentialCategory.EffectiveMessage,
                            EffectiveMessageDescription = outbreakPotentialCategory.EffectiveMessageDescription,
                            IsLocalTransmissionPossible = outbreakPotentialCategory.IsLocalTransmissionPossible,
                            MapThreshold = outbreakPotentialCategory.MapThreshold,
                            NeedsMap = outbreakPotentialCategory.NeedsMap,
                            Rule = outbreakPotentialCategory.Rule,
                            GeonameId = locationShape.GeonameId,
                            ProvinceGeonameId = locationShape.ProvinceGeonameId ?? 0
,
                            CountryGeonameId = locationShape.CountryGeonameId ?? 0
,
                            LocationDisplayName = locationShape.LocationDisplayName,
                            LocationType = locationShape.LocationType,
                            ShapeAsText = locationShape.ShapeAsText
                        });
                }
            }
            else
            {
                var eventLocationsShapes = zebraDbContext.usp_ZebraPlaceGetLocationShapeByGeonameId(aoiGeonameIds).ToList();

                foreach (var eventLocationShapes in eventLocationsShapes)
                {
                    if (outbreakPotentialCategory != null)
                        eventLocationsOutbreakPotentialModels.Add(new EventLocationsOutbreakPotentialModel
                        {
                            AttributeId = outbreakPotentialCategory.AttributeId,
                            EffectiveMessage = outbreakPotentialCategory.EffectiveMessage,
                            EffectiveMessageDescription = outbreakPotentialCategory.EffectiveMessageDescription,
                            IsLocalTransmissionPossible = outbreakPotentialCategory.IsLocalTransmissionPossible,
                            MapThreshold = outbreakPotentialCategory.MapThreshold,
                            NeedsMap = outbreakPotentialCategory.NeedsMap,
                            Rule = outbreakPotentialCategory.Rule,
                            GeonameId = eventLocationShapes.GeonameId,
                            LocationDisplayName = eventLocationShapes.LocationDisplayName,
                            LocationType = eventLocationShapes.LocationType,
                            ShapeAsText = eventLocationShapes.ShapeAsText
                        });
                }
            }
            return eventLocationsOutbreakPotentialModels;
        }
    }
}
