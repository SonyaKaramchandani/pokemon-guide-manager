using System;
using System.Collections.Generic;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.CustomModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class SourceAirportQueryBuilderDatabaseFixture : IDisposable
    {
        /*
         * TEST DATA OVERVIEW
         * 
         * Airports:
         * - Toronto YYZ:       Grids AA-##
         * - Taipei TPE:        Grids AB-##
         * - Vancouver YVR:     Grids AC-##
         * - Paris CDG:         No grids
         *
         * Events:
         * - Event ID 1:        TPE only
         * - Event ID 2:        CDG, TPE, YVR
         * - Event ID 11:       YYZ only                IsLocalOnly
         * - Event ID 12:       YVR only                Locations are only Countries
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

        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E11_AA_01 = new EventSourceGridSpreadMd {EventId = 11, Grid = GRID_AA_01, Cases = 112, MinCases = 25, MaxCases = 6234};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E11_AA_02 = new EventSourceGridSpreadMd {EventId = 11, Grid = GRID_AA_02, Cases = 3252, MinCases = 12, MaxCases = 563234};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E11_AA_03 = new EventSourceGridSpreadMd {EventId = 11, Grid = GRID_AA_03, Cases = 1364, MinCases = 523, MaxCases = 2524};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E11_AA_04 = new EventSourceGridSpreadMd {EventId = 11, Grid = GRID_AA_04, Cases = 2542, MinCases = 64, MaxCases = 3151};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E1_AB_01 = new EventSourceGridSpreadMd {EventId = 1, Grid = GRID_AB_01, Cases = 7643, MinCases = 134, MaxCases = 23515};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E1_AB_02 = new EventSourceGridSpreadMd {EventId = 1, Grid = GRID_AB_02, Cases = 7, MinCases = 1, MaxCases = 10};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E1_AB_03 = new EventSourceGridSpreadMd {EventId = 1, Grid = GRID_AB_03, Cases = 0, MinCases = 0, MaxCases = 0};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E2_AB_01 = new EventSourceGridSpreadMd {EventId = 2, Grid = GRID_AB_01, Cases = 7643, MinCases = 134, MaxCases = 23515};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E2_AB_02 = new EventSourceGridSpreadMd {EventId = 2, Grid = GRID_AB_02, Cases = 7, MinCases = 1, MaxCases = 10};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E2_AB_03 = new EventSourceGridSpreadMd {EventId = 2, Grid = GRID_AB_03, Cases = 0, MinCases = 0, MaxCases = 0};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E12_AC_01 = new EventSourceGridSpreadMd {EventId = 12, Grid = GRID_AC_01, Cases = 244, MinCases = 21, MaxCases = 6243};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E12_AC_02 = new EventSourceGridSpreadMd {EventId = 12, Grid = GRID_AC_02, Cases = 7, MinCases = 1, MaxCases = 10};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E12_AC_03 = new EventSourceGridSpreadMd {EventId = 12, Grid = GRID_AC_03, Cases = 0, MinCases = 0, MaxCases = 0};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E2_AC_01 = new EventSourceGridSpreadMd {EventId = 2, Grid = GRID_AC_01, Cases = 244, MinCases = 21, MaxCases = 6243};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E2_AC_02 = new EventSourceGridSpreadMd {EventId = 2, Grid = GRID_AC_02, Cases = 7, MinCases = 1, MaxCases = 10};
        public static readonly EventSourceGridSpreadMd SOURCE_GRID_E2_AC_03 = new EventSourceGridSpreadMd {EventId = 2, Grid = GRID_AC_03, Cases = 0, MinCases = 0, MaxCases = 0};

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

        public static readonly EventSourceAirportSpreadMd SOURCE_AIRPORT_A = new EventSourceAirportSpreadMd
        {
            SourceStation = STATION_YYZ,
            Event = EVENT_LOCAL_ONLY,
            Volume = null,
            MinProb = null,
            MaxProb = null,
            MinExpVolume = null,
            MaxExpVolume = null,
            MinPrevalence = null,
            MaxPrevalence = null
        };

        public static readonly EventSourceAirportSpreadMd SOURCE_AIRPORT_B = new EventSourceAirportSpreadMd
        {
            SourceStation = STATION_YVR,
            Event = EVENT_COUNTRY_ONLY,
            Volume = 0,
            MinProb = 0.0000m,
            MaxProb = 0.0000m,
            MinExpVolume = 0.0000m,
            MaxExpVolume = 0.0000m,
            MinPrevalence = 0.0000d,
            MaxPrevalence = 0.0000d
        };

        public static readonly EventSourceAirportSpreadMd SOURCE_AIRPORT_C = new EventSourceAirportSpreadMd
        {
            SourceStation = STATION_TPE,
            Event = EVENT_1,
            Volume = 235891,
            MinProb = 0.8789m,
            MaxProb = 0.9001m,
            MinExpVolume = 2.111m,
            MaxExpVolume = 2.303m,
            MinPrevalence = 0.000324118559945606d,
            MaxPrevalence = 0.000331892011424378d
        };

        public static readonly EventSourceAirportSpreadMd SOURCE_AIRPORT_D = new EventSourceAirportSpreadMd
        {
            SourceStation = STATION_TPE,
            Event = EVENT_2,
            Volume = 65437547,
            MinProb = 0.0089m,
            MaxProb = 0.0116m,
            MinExpVolume = 49.387m,
            MaxExpVolume = 50.519m,
            MinPrevalence = 0.000437885515885104d,
            MaxPrevalence = 0.000449198354277068d
        };

        public static readonly EventSourceAirportSpreadMd SOURCE_AIRPORT_E = new EventSourceAirportSpreadMd
        {
            SourceStation = STATION_YVR,
            Event = EVENT_2,
            Volume = 5362,
            MinProb = 0.2693m,
            MaxProb = 0.3143m,
            MinExpVolume = 9.569m,
            MaxExpVolume = 10.892m,
            MinPrevalence = 0.000000570776679617062d,
            MaxPrevalence = 0.000000844799627952716d
        };

        public static readonly EventSourceAirportSpreadMd SOURCE_AIRPORT_F = new EventSourceAirportSpreadMd
        {
            SourceStation = STATION_CDG,
            Event = EVENT_2,
            Volume = 1266754,
            MinProb = 0.0000m,
            MaxProb = 0.0440m,
            MinExpVolume = 750.739m,
            MaxExpVolume = 770.134m,
            MinPrevalence = 0.000092679626437237d,
            MaxPrevalence = 0.000103863727029108d
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

        public static readonly IEnumerable<object[]> CASE_COUNTS_TEST_DATA =
            new[]
            {
                // Non-existent event
                new object[]
                {
                    999,
                    new GridStationCaseJoinResult[] { }
                },

                // Single Airport
                new object[]
                {
                    1,
                    new[]
                    {
                        new[]
                        {
                            new GridStationCaseJoinResult {Probability = 0.2d, Cases = 7643, MinCases = 134, MaxCases = 23515},
                            new GridStationCaseJoinResult {Probability = 0.8d, Cases = 7, MinCases = 1, MaxCases = 10}
                        }
                    }
                },

                // Multiple Airport
                new object[]
                {
                    2,
                    new[]
                    {
                        new GridStationCaseJoinResult[0],
                        new[]
                        {
                            new GridStationCaseJoinResult {Probability = 0.2d, Cases = 7643, MinCases = 134, MaxCases = 23515},
                            new GridStationCaseJoinResult {Probability = 0.8d, Cases = 7, MinCases = 1, MaxCases = 10}
                        },
                        new[]
                        {
                            new GridStationCaseJoinResult {Probability = 0d, Cases = 0, MinCases = 0, MaxCases = 0},
                            new GridStationCaseJoinResult {Probability = 0.011d, Cases = 7, MinCases = 1, MaxCases = 10},
                            new GridStationCaseJoinResult {Probability = 0.989d, Cases = 244, MinCases = 21, MaxCases = 6243}
                        }
                    }
                },

                // Local Only
                new object[]
                {
                    11,
                    new[]
                    {
                        new[]
                        {
                            new GridStationCaseJoinResult {Probability = 0.5d, Cases = 3252, MinCases = 12, MaxCases = 563234},
                            new GridStationCaseJoinResult {Probability = 0.5d, Cases = 2542, MinCases = 64, MaxCases = 3151}
                        }
                    }
                },

                // Country Only
                new object[]
                {
                    12,
                    new[]
                    {
                        new[]
                        {
                            new GridStationCaseJoinResult {Probability = 0d, Cases = 0, MinCases = 0, MaxCases = 0},
                            new GridStationCaseJoinResult {Probability = 0.011d, Cases = 7, MinCases = 1, MaxCases = 10},
                            new GridStationCaseJoinResult {Probability = 0.989d, Cases = 244, MinCases = 21, MaxCases = 6243}
                        }
                    }
                }
            };

        public BiodZebraContext DbContext { get; }

        public SourceAirportQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("SourceAirportQueryBuilderDatabaseFixture")
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

            DbContext.EventSourceAirportSpreadMd.Add(SOURCE_AIRPORT_A);
            DbContext.EventSourceAirportSpreadMd.Add(SOURCE_AIRPORT_B);
            DbContext.EventSourceAirportSpreadMd.Add(SOURCE_AIRPORT_C);
            DbContext.EventSourceAirportSpreadMd.Add(SOURCE_AIRPORT_D);
            DbContext.EventSourceAirportSpreadMd.Add(SOURCE_AIRPORT_E);
            DbContext.EventSourceAirportSpreadMd.Add(SOURCE_AIRPORT_F);

            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E11_AA_01);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E11_AA_02);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E11_AA_03);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E11_AA_04);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E1_AB_01);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E1_AB_02);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E1_AB_03);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E2_AB_01);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E2_AB_02);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E2_AB_03);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E12_AC_01);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E12_AC_02);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E12_AC_03);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E2_AC_01);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E2_AC_02);
            DbContext.EventSourceGridSpreadMd.Add(SOURCE_GRID_E2_AC_03);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}