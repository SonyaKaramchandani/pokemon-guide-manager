using System;
using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class DestinationAirportQueryBuilderDatabaseFixture : IDisposable
    {
        /*
         * TEST DATA OVERVIEW
         * 
         * Airports:
         * - Toronto YYZ:         Grids AA-##
         * - Taipei TPE:          Grids AB-##
         * - Vancouver YVR:       Grids AC-##
         * - Paris CDG:           No grids
         *
         * Events:
         * - Event ID 1:          TPE only
         * - Event ID 2:          CDG, TPE, YVR
         * - Event ID 11:         YYZ only          IsLocalOnly
         * - Event ID 12:         YVR only          Locations are only Countries
         *
         * Geonames:
         * - Geoname ID 1:        City              Grids AA-##
         * - Geoname ID 2:        City              Grids AB-##
         * - Geoname ID 3:        City              Grids AA-##, AB-##
         * - Geoname ID 4:        City              Grids AA-##, AB-##, AC-##
         * - Geoname ID 5:        Province          Grids AC-##
         * - Geoname ID 6:        Province          Grids AA-##
         * - Geoname ID 7:        Province          Grids AB-##, AC-##
         * - Geoname ID 8:        Province          Grids AA-##, AB-##, AC-##
         * - Geoname ID 9:        Country           Grids AB-##
         * - Geoname ID 10:       Country           Grids AC-##
         * - Geoname ID 11:       Country           Grids AA-##, AC-##
         * - Geoname ID 12:       Country           Grids AA-##, AB-##, AC-##
         */
        public static readonly Huffmodel25kmworldhexagon GRID_AA_01 = new Huffmodel25kmworldhexagon {GridId = "AA-01", Population = 0};
        public static readonly Huffmodel25kmworldhexagon GRID_AA_02 = new Huffmodel25kmworldhexagon {GridId = "AA-02", Population = 1294871};
        public static readonly Huffmodel25kmworldhexagon GRID_AA_03 = new Huffmodel25kmworldhexagon {GridId = "AA-03", Population = 235078235};
        public static readonly Huffmodel25kmworldhexagon GRID_AA_04 = new Huffmodel25kmworldhexagon {GridId = "AA-04", Population = 0};
        public static readonly Huffmodel25kmworldhexagon GRID_AB_01 = new Huffmodel25kmworldhexagon {GridId = "AB-01", Population = 1294871};
        public static readonly Huffmodel25kmworldhexagon GRID_AB_02 = new Huffmodel25kmworldhexagon {GridId = "AB-02", Population = 235078235};
        public static readonly Huffmodel25kmworldhexagon GRID_AB_03 = new Huffmodel25kmworldhexagon {GridId = "AB-03", Population = 0};
        public static readonly Huffmodel25kmworldhexagon GRID_AC_01 = new Huffmodel25kmworldhexagon {GridId = "AC-01", Population = 0};
        public static readonly Huffmodel25kmworldhexagon GRID_AC_02 = new Huffmodel25kmworldhexagon {GridId = "AC-02", Population = 1294871};
        public static readonly Huffmodel25kmworldhexagon GRID_AC_03 = new Huffmodel25kmworldhexagon {GridId = "AC-03", Population = 235078235};

        public static readonly GridStation GRID_STATION_1 = new GridStation {Probability = 0.5m, ValidFromDate = DateTime.Today.AddMonths(1), Grid = GRID_AA_01};
        public static readonly GridStation GRID_STATION_2 = new GridStation {Probability = 0.5m, ValidFromDate = DateTime.Today, Grid = GRID_AA_02};
        public static readonly GridStation GRID_STATION_3 = new GridStation {Probability = 0.5m, ValidFromDate = DateTime.Today.AddMonths(-1), Grid = GRID_AA_03};
        public static readonly GridStation GRID_STATION_4 = new GridStation {Probability = 0.5m, ValidFromDate = DateTime.Today, Grid = GRID_AA_04};
        public static readonly GridStation GRID_STATION_5 = new GridStation {Probability = 0.2m, ValidFromDate = DateTime.Today, Grid = GRID_AB_01};
        public static readonly GridStation GRID_STATION_6 = new GridStation {Probability = 0.8m, ValidFromDate = DateTime.Today, Grid = GRID_AB_02};
        public static readonly GridStation GRID_STATION_7 = new GridStation {Probability = 0.9m, ValidFromDate = DateTime.Today.AddMonths(1), Grid = GRID_AB_03};
        public static readonly GridStation GRID_STATION_8 = new GridStation {Probability = 0.989m, ValidFromDate = DateTime.Today, Grid = GRID_AC_01};
        public static readonly GridStation GRID_STATION_9 = new GridStation {Probability = 0.011m, ValidFromDate = DateTime.Today, Grid = GRID_AC_02};
        public static readonly GridStation GRID_STATION_10 = new GridStation {Probability = 0m, ValidFromDate = DateTime.Today, Grid = GRID_AC_03};

        public static readonly Stations STATION_YYZ = new Stations
        {
            StationId = 10357,
            StationCode = "YYZ",
            StationGridName = "Toronto Pearson International Airport",
            Latitude = 43.68066m,
            Longitude = -79.61286m,
            CityGeoname = new Geonames {GeonameId = 6167865, DisplayName = "Toronto, Ontario, Canada"},
            GridStation = new[]
            {
                GRID_STATION_1,
                GRID_STATION_2,
                GRID_STATION_3,
                GRID_STATION_4
            }
        };

        public static readonly Stations STATION_YVR = new Stations
        {
            StationId = 10711,
            StationCode = "YVR",
            StationGridName = "Vancouver International Airport",
            Latitude = 49.19489m,
            Longitude = -123.17923m,
            CityGeoname = new Geonames {GeonameId = 6173331, DisplayName = "Vancouver, British Columbia, Canada"},
            GridStation = new[]
            {
                GRID_STATION_8,
                GRID_STATION_9,
                GRID_STATION_10
            }
        };

        public static readonly Stations STATION_TPE = new Stations
        {
            StationId = 9968,
            StationCode = "TPE",
            StationGridName = "Taiwan Taoyuan International Airport",
            Latitude = 25.07773m,
            Longitude = 121.23282m,
            CityGeoname = new Geonames {GeonameId = 1668341, DisplayName = "Taipei, Taiwan, Taiwan"},
            GridStation = new[]
            {
                GRID_STATION_5,
                GRID_STATION_6,
                GRID_STATION_7
            }
        };

        public static readonly Stations STATION_CDG = new Stations
        {
            StationId = 7740,
            StationCode = "CDG",
            StationGridName = "Paris Charles de Gaulle Airport",
            Latitude = 49.01278m,
            Longitude = 2.55000m,
            CityGeoname = null,
            GridStation = new List<GridStation>()
        };

        public static readonly Event EVENT_LOCAL_ONLY = new Event
        {
            EventId = 11,
            IsLocalOnly = true,
            XtblEventLocation = new List<XtblEventLocation>
            {
                new XtblEventLocation {Geoname = new ActiveGeonames {GeonameId = 101, LocationType = (int) LocationType.City}}
            }
        };

        public static readonly Event EVENT_COUNTRY_ONLY = new Event
        {
            EventId = 12,
            IsLocalOnly = false,
            XtblEventLocation = new List<XtblEventLocation>
            {
                new XtblEventLocation {Geoname = new ActiveGeonames {GeonameId = 301, LocationType = (int) LocationType.Country}},
                new XtblEventLocation {Geoname = new ActiveGeonames {GeonameId = 302, LocationType = (int) LocationType.Country}}
            }
        };

        public static readonly Event EVENT_1 = new Event
        {
            EventId = 1,
            IsLocalOnly = false,
            XtblEventLocation = new List<XtblEventLocation>
            {
                new XtblEventLocation {Geoname = new ActiveGeonames {GeonameId = 102, LocationType = (int) LocationType.City}},
                new XtblEventLocation {Geoname = new ActiveGeonames {GeonameId = 201, LocationType = (int) LocationType.Province}}
            }
        };

        public static readonly Event EVENT_2 = new Event
        {
            EventId = 2,
            IsLocalOnly = false,
            XtblEventLocation = new List<XtblEventLocation>
            {
                new XtblEventLocation {Geoname = new ActiveGeonames {GeonameId = 103, LocationType = (int) LocationType.City}},
                new XtblEventLocation {Geoname = new ActiveGeonames {GeonameId = 202, LocationType = (int) LocationType.Province}}
            }
        };

        public static readonly EventDestinationAirportSpreadMd DESTINATION_AIRPORT_A = new EventDestinationAirportSpreadMd
        {
            DestinationStation = STATION_YYZ,
            Event = EVENT_LOCAL_ONLY,
            Volume = null,
            MinProb = null,
            MaxProb = null,
            MinExpVolume = null,
            MaxExpVolume = null
        };

        public static readonly EventDestinationAirportSpreadMd DESTINATION_AIRPORT_B = new EventDestinationAirportSpreadMd
        {
            DestinationStation = STATION_YVR,
            Event = EVENT_COUNTRY_ONLY,
            Volume = 0,
            MinProb = 0.0000m,
            MaxProb = 0.0000m,
            MinExpVolume = 0.0000m,
            MaxExpVolume = 0.0000m
        };

        public static readonly EventDestinationAirportSpreadMd DESTINATION_AIRPORT_C = new EventDestinationAirportSpreadMd
        {
            DestinationStation = STATION_TPE,
            Event = EVENT_1,
            Volume = 235891,
            MinProb = 0.8789m,
            MaxProb = 0.9001m,
            MinExpVolume = 2.111m,
            MaxExpVolume = 2.303m
        };

        public static readonly EventDestinationAirportSpreadMd DESTINATION_AIRPORT_D = new EventDestinationAirportSpreadMd
        {
            DestinationStation = STATION_TPE,
            Event = EVENT_2,
            Volume = 65437547,
            MinProb = 0.0089m,
            MaxProb = 0.0116m,
            MinExpVolume = 49.387m,
            MaxExpVolume = 50.519m
        };

        public static readonly EventDestinationAirportSpreadMd DESTINATION_AIRPORT_E = new EventDestinationAirportSpreadMd
        {
            DestinationStation = STATION_YVR,
            Event = EVENT_2,
            Volume = 5362,
            MinProb = 0.2693m,
            MaxProb = 0.3143m,
            MinExpVolume = 9.569m,
            MaxExpVolume = 10.892m
        };

        public static readonly EventDestinationAirportSpreadMd DESTINATION_AIRPORT_F = new EventDestinationAirportSpreadMd
        {
            DestinationStation = STATION_CDG,
            Event = EVENT_2,
            Volume = 1266754,
            MinProb = 0.0000m,
            MaxProb = 0.0440m,
            MinExpVolume = 750.739m,
            MaxExpVolume = 770.134m
        };

        public static readonly IEnumerable<object[]> CITY_NAMES_TEST_DATA =
            new[]
            {
                // Non-existent event
                new object[]
                {
                    999,
                    new int?[0],
                    new string[0]
                },

                // Single Airport
                new object[]
                {
                    1,
                    new int?[] {1668341},
                    new[] {"Taipei, Taiwan, Taiwan"}
                },

                // Multiple Airport
                new object[]
                {
                    2,
                    new int?[] {null, 1668341, 6173331},
                    new[] {null, "Taipei, Taiwan, Taiwan", "Vancouver, British Columbia, Canada"}
                },

                // Local Only
                new object[]
                {
                    11,
                    new int?[] {6167865},
                    new[] {"Toronto, Ontario, Canada"}
                },

                // Country Only
                new object[]
                {
                    12,
                    new int?[] {6173331},
                    new[] {"Vancouver, British Columbia, Canada"}
                }
            };

        public BiodZebraContext DbContext { get; }

        public DestinationAirportQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("DestinationAirportQueryBuilderDatabaseFixture")
                .EnableSensitiveDataLogging()
                .Options);

            DbContext.GridStation.Add(GRID_STATION_1);
            DbContext.GridStation.Add(GRID_STATION_2);
            DbContext.GridStation.Add(GRID_STATION_3);
            DbContext.GridStation.Add(GRID_STATION_4);
            DbContext.GridStation.Add(GRID_STATION_5);
            DbContext.GridStation.Add(GRID_STATION_6);
            DbContext.GridStation.Add(GRID_STATION_7);
            DbContext.GridStation.Add(GRID_STATION_8);
            DbContext.GridStation.Add(GRID_STATION_9);
            DbContext.GridStation.Add(GRID_STATION_10);

            DbContext.EventDestinationAirportSpreadMd.Add(DESTINATION_AIRPORT_A);
            DbContext.EventDestinationAirportSpreadMd.Add(DESTINATION_AIRPORT_B);
            DbContext.EventDestinationAirportSpreadMd.Add(DESTINATION_AIRPORT_C);
            DbContext.EventDestinationAirportSpreadMd.Add(DESTINATION_AIRPORT_D);
            DbContext.EventDestinationAirportSpreadMd.Add(DESTINATION_AIRPORT_E);
            DbContext.EventDestinationAirportSpreadMd.Add(DESTINATION_AIRPORT_F);

            DbContext.Geonames.Add(new Geonames {GeonameId = 1, LocationType = (int) LocationType.City});
            DbContext.Geonames.Add(new Geonames {GeonameId = 2, LocationType = (int) LocationType.City});
            DbContext.Geonames.Add(new Geonames {GeonameId = 3, LocationType = (int) LocationType.City});
            DbContext.Geonames.Add(new Geonames {GeonameId = 4, LocationType = (int) LocationType.City});
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 5, LocationType = (int) LocationType.Province, GridProvince = new[]
                {
                    new GridProvince {GridId = "AC-01"},
                    new GridProvince {GridId = "AC-02"},
                    new GridProvince {GridId = "AC-03"}
                }
            });
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 6, LocationType = (int) LocationType.Province, GridProvince = new[]
                {
                    new GridProvince {GridId = "AA-01"},
                    new GridProvince {GridId = "AA-02"},
                    new GridProvince {GridId = "AA-03"},
                    new GridProvince {GridId = "AA-04"}
                }
            });
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 7, LocationType = (int) LocationType.Province, GridProvince = new[]
                {
                    new GridProvince {GridId = "AB-01"},
                    new GridProvince {GridId = "AB-02"},
                    new GridProvince {GridId = "AB-03"},
                    new GridProvince {GridId = "AC-01"},
                    new GridProvince {GridId = "AC-02"},
                    new GridProvince {GridId = "AC-03"}
                }
            });
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 8, LocationType = (int) LocationType.Province, GridProvince = new[]
                {
                    new GridProvince {GridId = "AA-01"},
                    new GridProvince {GridId = "AA-02"},
                    new GridProvince {GridId = "AA-03"},
                    new GridProvince {GridId = "AA-04"},
                    new GridProvince {GridId = "AB-01"},
                    new GridProvince {GridId = "AB-02"},
                    new GridProvince {GridId = "AB-03"},
                    new GridProvince {GridId = "AC-01"},
                    new GridProvince {GridId = "AC-02"},
                    new GridProvince {GridId = "AC-03"}
                }
            });
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 9, LocationType = (int) LocationType.Country, GridCountry = new[]
                {
                    new GridCountry {GridId = "AB-01"},
                    new GridCountry {GridId = "AB-02"},
                    new GridCountry {GridId = "AB-03"}
                }
            });
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 10, LocationType = (int) LocationType.Country, GridCountry = new[]
                {
                    new GridCountry {GridId = "AC-01"},
                    new GridCountry {GridId = "AC-02"},
                    new GridCountry {GridId = "AC-03"}
                }
            });
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 11, LocationType = (int) LocationType.Country, GridCountry = new[]
                {
                    new GridCountry {GridId = "AA-01"},
                    new GridCountry {GridId = "AA-02"},
                    new GridCountry {GridId = "AA-03"},
                    new GridCountry {GridId = "AA-04"},
                    new GridCountry {GridId = "AC-01"},
                    new GridCountry {GridId = "AC-02"},
                    new GridCountry {GridId = "AC-03"}
                }
            });
            DbContext.Geonames.Add(new Geonames
            {
                GeonameId = 12, LocationType = (int) LocationType.Country, GridCountry = new[]
                {
                    new GridCountry {GridId = "AA-01"},
                    new GridCountry {GridId = "AA-02"},
                    new GridCountry {GridId = "AA-03"},
                    new GridCountry {GridId = "AA-04"},
                    new GridCountry {GridId = "AB-01"},
                    new GridCountry {GridId = "AB-02"},
                    new GridCountry {GridId = "AB-03"},
                    new GridCountry {GridId = "AC-01"},
                    new GridCountry {GridId = "AC-02"},
                    new GridCountry {GridId = "AC-03"}
                }
            });

            DbContext.usp_ZebraPlaceGetGridIdByGeonameId_Result = DbContext.usp_ZebraPlaceGetGridIdByGeonameId_Result
                .MockFromSql(callExpression => GetGrids((object[]) callExpression.ElementAtOrDefault(2)).AsQueryable());

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        private static IEnumerable<usp_ZebraPlaceGetGridIdByGeonameId_Result> GetGrids(IReadOnlyList<object> args)
        {
            return (int) args[0] switch
            {
                1 => new[]
                {
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-01"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-02"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-03"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-04"}
                },
                2 => new[]
                {
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-01"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-02"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-03"}
                },
                3 => new[]
                {
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-01"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-02"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-03"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-04"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-01"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-02"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-03"}
                },
                _ => new[]
                {
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-01"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-02"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-03"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AA-04"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-01"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-02"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AB-03"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AC-01"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AC-02"},
                    new usp_ZebraPlaceGetGridIdByGeonameId_Result {GridId = "AC-03"}
                }
            };
        }
    }
}