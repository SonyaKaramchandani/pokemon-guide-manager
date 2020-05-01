using System.Collections.Generic;
using Biod.Products.Common.Constants;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.Event;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public class OrderingHelperTestData
    {
        private static readonly GetEventModel LocalEvent = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 0},
            IsLocal = true,
            ExportationRisk = RiskModelFactory(1, 100),
        };

        private static readonly GetEventModel EventA = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 1},
            ExportationRisk = RiskModelFactory(1, 100),
            ImportationRisk = RiskModelFactory(1, 100)
        };

        private static readonly GetEventModel EventB = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 2},
            ExportationRisk = RiskModelFactory(1, 50),
            ImportationRisk = RiskModelFactory(1, 100)
        };

        private static readonly GetEventModel EventC = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 3},
            ExportationRisk = RiskModelFactory(0.5f, 100),
            ImportationRisk = RiskModelFactory(1, 100)
        };

        private static readonly GetEventModel EventD = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 4},
            ExportationRisk = RiskModelFactory(0.5f, 50),
            ImportationRisk = RiskModelFactory(1, 100)
        };

        private static readonly GetEventModel EventE = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 5},
            ExportationRisk = RiskModelFactory(1, 100),
            ImportationRisk = RiskModelFactory(1, 50)
        };

        private static readonly GetEventModel EventF = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 6},
            ExportationRisk = RiskModelFactory(1, 100),
            ImportationRisk = RiskModelFactory(0.5f, 50)
        };

        private static readonly GetEventModel EventG = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 7},
            ExportationRisk = RiskModelFactory(1, 100),
            ImportationRisk = null
        };

        private static readonly GetEventModel EventH = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 8},
            ExportationRisk = RiskModelFactory(1, 50),
            ImportationRisk = null
        };

        private static readonly GetEventModel EventI = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 9},
            ExportationRisk = RiskModelFactory(0.5f, 100),
            ImportationRisk = null
        };

        private static readonly GetEventModel EventJ = new GetEventModel
        {
            EventInformation = new EventInformationModel {Id = 10},
            ExportationRisk = RiskModelFactory(0.5f, 50),
            ImportationRisk = null
        };

        private static readonly EventLocationModel CountryA = new EventLocationModel {GeonameId = 1, LocationType = (int) LocationType.Country, LocationName = "Afghanistan"};
        private static readonly EventLocationModel CountryB = new EventLocationModel {GeonameId = 2, LocationType = (int) LocationType.Country, LocationName = "Bahamas"};
        private static readonly EventLocationModel CountryC = new EventLocationModel {GeonameId = 3, LocationType = (int) LocationType.Country, LocationName = "Canada"};
        private static readonly EventLocationModel CountryD = new EventLocationModel {GeonameId = 4, LocationType = (int) LocationType.Country, LocationName = "Denmark"};
        private static readonly EventLocationModel CountryE = new EventLocationModel {GeonameId = 5, LocationType = (int) LocationType.Country, LocationName = "Ethiopia"};

        private static readonly EventLocationModel ProvinceA = new EventLocationModel {GeonameId = 6, LocationType = (int) LocationType.Province, LocationName = "Alberta", ProvinceName = "Alberta"};
        private static readonly EventLocationModel ProvinceB = new EventLocationModel {GeonameId = 7, LocationType = (int) LocationType.Province, LocationName = "BC", ProvinceName = "BC"};
        private static readonly EventLocationModel ProvinceC = new EventLocationModel {GeonameId = 8, LocationType = (int) LocationType.Province, LocationName = "Cali", ProvinceName = "Cali"};
        private static readonly EventLocationModel ProvinceD = new EventLocationModel {GeonameId = 9, LocationType = (int) LocationType.Province, LocationName = "Delaware", ProvinceName = "Delaware"};
        private static readonly EventLocationModel ProvinceE = new EventLocationModel {GeonameId = 10, LocationType = (int) LocationType.Province, LocationName = "East", ProvinceName = "East"};

        private static readonly EventLocationModel CityA = new EventLocationModel {GeonameId = 11, LocationType = (int) LocationType.City, LocationName = "Ajax", ProvinceName = "Ontario"};
        private static readonly EventLocationModel CityB = new EventLocationModel {GeonameId = 12, LocationType = (int) LocationType.City, LocationName = "Barrie", ProvinceName = "Ontario"};
        private static readonly EventLocationModel CityC = new EventLocationModel {GeonameId = 13, LocationType = (int) LocationType.City, LocationName = "Chicago", ProvinceName = "Illinois"};
        private static readonly EventLocationModel CityD = new EventLocationModel {GeonameId = 14, LocationType = (int) LocationType.City, LocationName = "Downtown Toronto", ProvinceName = "Ontario"};
        private static readonly EventLocationModel CityE = new EventLocationModel {GeonameId = 15, LocationType = (int) LocationType.City, LocationName = "Edmonton", ProvinceName = "Alberta"};

        private static RiskModel RiskModelFactory(float maxProb, float maxVolume)
        {
            return new RiskModel
            {
                MaxProbability = maxProb,
                MaxMagnitude = maxVolume
            };
        }

        public static IEnumerable<object[]> OrderEventData = new[]
        {
            // Empty
            new object[]
            {
                new GetEventModel[0],
                new int[0]
            },

            // Local vs Non-local
            new object[]
            {
                new[] {EventB, EventH, LocalEvent},
                new[] {0, 2, 8}
            },

            // Same importation, different exportation
            new object[]
            {
                new[] {EventC, EventA, EventB, EventD},
                new[] {1, 2, 3, 4}
            },

            // Different importation, same exportation
            new object[]
            {
                new[] {EventF, EventA, EventE},
                new[] {1, 5, 6}
            },

            // Null importation, different exportation
            new object[]
            {
                new[] {EventJ, EventG, EventH, EventI},
                new[] {7, 8, 9, 10}
            },

            // All
            new object[]
            {
                new[] {EventJ, EventE, EventA, EventG, EventC, EventF, EventH, EventB, EventI, EventD, LocalEvent},
                new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
            }
        };

        public static IEnumerable<object[]> OrderLocationData = new[]
        {
            // Empty
            new object[]
            {
                new EventLocationModel[0],
                new int[0]
            },

            // Country Only 
            new object[]
            {
                new[] {CountryE, CountryC, CountryA, CountryD, CountryB},
                new[] {1, 2, 3, 4, 5}
            },

            // Province Only
            new object[]
            {
                new[] {ProvinceB, ProvinceC, ProvinceD, ProvinceE, ProvinceA},
                new[] {6, 7, 8, 9, 10}
            },

            // City Only (by province, then by city name)
            new object[]
            {
                new[] {CityA, CityB, CityC, CityD, CityE},
                new[] {15, 13, 11, 12, 14}
            },

            // Country before Province before City
            new object[]
            {
                new[] {CityA, CityA, ProvinceB, ProvinceB, CountryE, CountryE},
                new[] {5, 5, 7, 7, 11, 11}
            },

            // All
            new object[]
            {
                new[] {CityA, CityB, CityC, CityD, CityE, ProvinceA, ProvinceB, ProvinceC, ProvinceD, ProvinceE, CountryA, CountryB, CountryC, CountryD, CountryE},
                new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 13, 11, 12, 14}
            }
        };
    }
}