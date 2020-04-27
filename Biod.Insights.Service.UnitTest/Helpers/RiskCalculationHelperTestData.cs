using System.Collections.Generic;
using Biod.Products.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.CustomModels;

namespace Biod.Insights.Service.UnitTest.Helpers
{
    public sealed class RiskCalculationHelperTestData
    {
        private static readonly Event LocalEvent = new Event {IsLocalOnly = true};
        private static readonly Event NonLocalEvent = new Event {IsLocalOnly = false};
        private static readonly XtblEventLocationJoinResult CountryEventLocation = new XtblEventLocationJoinResult {LocationType = (int) LocationType.Country};
        private static readonly XtblEventLocationJoinResult NonCountryEventLocation = new XtblEventLocationJoinResult {LocationType = (int) LocationType.City};

        private static EventImportationRisksByGeonameSpreadMd ImportationRiskFactory(
            decimal? minVolume,
            decimal? maxVolume,
            decimal? minProbability,
            decimal? maxProbability)
        {
            return new EventImportationRisksByGeonameSpreadMd {MinVolume = minVolume, MaxVolume = maxVolume, MinProb = minProbability, MaxProb = maxProbability};
        }

        private static EventExtensionSpreadMd ExportationRiskFactory(
            decimal? minVolume,
            decimal? maxVolume,
            decimal? minProbability,
            decimal? maxProbability)
        {
            return new EventExtensionSpreadMd
            {
                MinExportationVolumeViaAirports = minVolume,
                MaxExportationVolumeViaAirports = maxVolume,
                MinExportationProbabilityViaAirports = minProbability,
                MaxExportationProbabilityViaAirports = maxProbability
            };
        }

        /// <summary>
        /// Set of data that will yield the the flag for IsModelNotRun as true
        /// </summary>
        public static IEnumerable<object[]> NoModelsRun = new[]
        {
            // Empty List
            new[] {new List<EventJoinResult>()},

            // Single Event marked with model not run
            new[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult {Event = LocalEvent, XtblEventLocations = new XtblEventLocationJoinResult[0]}
                }
            },

            // Single Event with single event location at country level
            new[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult {Event = NonLocalEvent, XtblEventLocations = new[] {CountryEventLocation}}
                }
            },

            // Single Event with multiple event locations all at country level
            new[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult {Event = NonLocalEvent, XtblEventLocations = new[] {CountryEventLocation, CountryEventLocation, CountryEventLocation}}
                }
            },

            // Multiple Events, all are either with model not run or event locations all at country level
            new[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult {Event = LocalEvent, XtblEventLocations = new XtblEventLocationJoinResult[0]},
                    new EventJoinResult {Event = LocalEvent, XtblEventLocations = new XtblEventLocationJoinResult[0]},
                    new EventJoinResult {Event = NonLocalEvent, XtblEventLocations = new[] {CountryEventLocation}},
                    new EventJoinResult {Event = NonLocalEvent, XtblEventLocations = new[] {CountryEventLocation, CountryEventLocation, CountryEventLocation}}
                }
            }
        };

        /// <summary>
        /// Set of data that has risk model values for Importation Risk tests
        /// </summary>
        public static IEnumerable<object[]> ImportationRiskModels = new[]
        {
            // Single event with null values for risk
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = NonLocalEvent,
                        XtblEventLocations = new[] {NonCountryEventLocation},
                        ImportationRisk = ImportationRiskFactory(null, null, null, null)
                    }
                },
                0f, 0f, 0f, 0f
            },

            // Single event with 0 values for risk 
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = NonLocalEvent,
                        XtblEventLocations = new[] {NonCountryEventLocation},
                        ImportationRisk = ImportationRiskFactory(0, 0, 0, 0)
                    }
                },
                0f, 0f, 0f, 0f
            },

            // Single event
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = NonLocalEvent,
                        XtblEventLocations = new[] {NonCountryEventLocation},
                        ImportationRisk = ImportationRiskFactory(1.2345m, 5.6789m, 0.123m, 0.567m)
                    }
                },
                1.2345f, 5.6789f, 0.123f, 0.567f
            },

            // Multiple events, but only 1 is included
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult {Event = NonLocalEvent, XtblEventLocations = new XtblEventLocationJoinResult[0]},
                    new EventJoinResult {Event = LocalEvent, XtblEventLocations = new[] {CountryEventLocation}},
                    new EventJoinResult
                    {
                        Event = NonLocalEvent,
                        XtblEventLocations = new[] {NonCountryEventLocation},
                        ImportationRisk = ImportationRiskFactory(1.2345m, 5.6789m, 0.123m, 0.567m)
                    }
                },
                1.2345f, 5.6789f, 0.123f, 0.567f
            },

            // Multiple events
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = NonLocalEvent,
                        XtblEventLocations = new[] {NonCountryEventLocation},
                        ImportationRisk = ImportationRiskFactory(1.2345m, 5.6789m, 0.123m, 0.567m)
                    },
                    new EventJoinResult
                    {
                        Event = NonLocalEvent,
                        XtblEventLocations = new[] {NonCountryEventLocation},
                        ImportationRisk = ImportationRiskFactory(12.4679m, 42.4987m, 0.001m, 0.021m)
                    },
                    new EventJoinResult
                    {
                        Event = NonLocalEvent,
                        XtblEventLocations = new[] {NonCountryEventLocation},
                        ImportationRisk = ImportationRiskFactory(121.3764m, 396.2481m, 0.64m, 0.69m)
                    }
                },
                135.0788f, 444.4257f, 0.68459572f, 0.86858883f
            }
        };

        /// <summary>
        /// Set of data that has risk model values for Exportation Risk tests
        /// </summary>
        public static IEnumerable<object[]> ExportationRiskModels = new[]
        {
            // Single event with null values for risk
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = new Event
                        {
                            IsLocalOnly = false,
                            EventExtensionSpreadMd = ExportationRiskFactory(null, null, null, null)
                        },
                        XtblEventLocations = new[] {NonCountryEventLocation}
                    }
                },
                0f, 0f, 0f, 0f
            },

            // Single event with 0 values for risk 
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = new Event
                        {
                            IsLocalOnly = false,
                            EventExtensionSpreadMd = ExportationRiskFactory(0, 0, 0, 0)
                        },
                        XtblEventLocations = new[] {NonCountryEventLocation}
                    }
                },
                0f, 0f, 0f, 0f
            },

            // Single event
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = new Event
                        {
                            IsLocalOnly = false,
                            EventExtensionSpreadMd = ExportationRiskFactory(1.2345m, 5.6789m, 0.123m, 0.567m)
                        },
                        XtblEventLocations = new[] {NonCountryEventLocation}
                    }
                },
                1.2345f, 5.6789f, 0.123f, 0.567f
            },

            // Multiple events, but only 1 is included
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult {Event = NonLocalEvent, XtblEventLocations = new XtblEventLocationJoinResult[0]},
                    new EventJoinResult {Event = LocalEvent, XtblEventLocations = new[] {CountryEventLocation}},
                    new EventJoinResult
                    {
                        Event = new Event
                        {
                            IsLocalOnly = false,
                            EventExtensionSpreadMd = ExportationRiskFactory(1.2345m, 5.6789m, 0.123m, 0.567m)
                        },
                        XtblEventLocations = new[] {NonCountryEventLocation}
                    }
                },
                1.2345f, 5.6789f, 0.123f, 0.567f
            },

            // Multiple events
            new object[]
            {
                new List<EventJoinResult>
                {
                    new EventJoinResult
                    {
                        Event = new Event
                        {
                            IsLocalOnly = false,
                            EventExtensionSpreadMd = ExportationRiskFactory(1.2345m, 5.6789m, 0.123m, 0.567m)
                        },
                        XtblEventLocations = new[] {NonCountryEventLocation}
                    },
                    new EventJoinResult
                    {
                        Event = new Event
                        {
                            IsLocalOnly = false,
                            EventExtensionSpreadMd = ExportationRiskFactory(12.4679m, 42.4987m, 0.001m, 0.021m)
                        },
                        XtblEventLocations = new[] {NonCountryEventLocation}
                    },
                    new EventJoinResult
                    {
                        Event = new Event
                        {
                            IsLocalOnly = false,
                            EventExtensionSpreadMd = ExportationRiskFactory(121.3764m, 396.2481m, 0.64m, 0.69m)
                        },
                        XtblEventLocations = new[] {NonCountryEventLocation}
                    }
                },
                135.0788f, 444.4257f, 0.68459572f, 0.86858883f
            }
        };
    }
}