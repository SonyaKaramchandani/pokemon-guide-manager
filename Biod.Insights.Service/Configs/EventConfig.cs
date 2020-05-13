using System;
using System.Collections.Generic;

namespace Biod.Insights.Service.Configs
{
    public class EventConfig
    {
        public readonly int? EventId;

        public readonly int? GeonameId;
        public readonly HashSet<int> DiseaseIds;

        public readonly bool IncludeArticles;
        public readonly bool IncludeLocalCaseCount;
        public readonly bool IncludeLocations;
        public readonly bool IncludeProximalLocations;
        public readonly bool IncludeLocationsHistory;
        public readonly bool IncludeExportationRisk;
        public readonly bool IncludeImportationRisk;
        public readonly SourceAirportConfig SourceAirportConfig;
        public readonly bool IncludeSourceAirports;
        public readonly AirportConfig DestinationAirportConfig;
        public readonly bool IncludeDestinationAirports;
        public readonly bool IncludeDiseaseInformation;
        public readonly bool IncludeOutbreakPotential;
        public readonly bool IncludeCalculationMetadata;

        private EventConfig(Builder builder)
        {
            EventId = builder.EventId;
            GeonameId = builder.GeonameId;
            DiseaseIds = builder.DiseaseIds;
            IncludeArticles = builder.IncludeArticles;
            IncludeLocalCaseCount = builder.IncludeLocalCaseCount;
            IncludeLocations = builder.IncludeLocations;
            IncludeProximalLocations = builder.IncludeProximalLocations;
            IncludeLocationsHistory = builder.IncludeLocationsHistory;
            IncludeExportationRisk = builder.IncludeExportationRisk;
            IncludeImportationRisk = builder.IncludeImportationRisk;
            SourceAirportConfig = builder.SourceAirportConfig;
            IncludeSourceAirports = builder.SourceAirportConfig != null;
            DestinationAirportConfig = builder.DestinationAirportConfig;
            IncludeDestinationAirports = builder.DestinationAirportConfig != null;
            IncludeDiseaseInformation = builder.IncludeDiseaseInformation;
            IncludeOutbreakPotential = builder.GeonameId.HasValue;
            IncludeCalculationMetadata = builder.IncludeCalculationMetadata;
        }


        public class Builder
        {
            private int? _geonameId;
            protected internal int? GeonameId
            {
                get => _geonameId;
                private set
                {
                    if (_geonameId.HasValue && _geonameId.Value != value)
                    {
                        throw new InvalidOperationException($"The geonameId has already been set to a different value of {_geonameId.Value}");
                    }

                    _geonameId = value;
                }
            }

            protected internal int? EventId;
            protected internal readonly HashSet<int> DiseaseIds = new HashSet<int>();
            protected internal bool IncludeArticles;
            protected internal bool IncludeLocalCaseCount;
            protected internal bool IncludeLocations;
            protected internal bool IncludeProximalLocations;
            protected internal bool IncludeLocationsHistory;
            protected internal bool IncludeExportationRisk;
            protected internal bool IncludeImportationRisk;
            protected internal SourceAirportConfig SourceAirportConfig;
            protected internal AirportConfig DestinationAirportConfig;
            protected internal bool IncludeDiseaseInformation;
            protected internal bool IncludeCalculationMetadata;

            public Builder SetEventId(int eventId)
            {
                EventId = eventId;
                return this;
            }

            public Builder AddDiseaseId(int diseaseId)
            {
                DiseaseIds.Add(diseaseId);
                return this;
            }

            public Builder AddDiseaseIds(IEnumerable<int> diseaseIds)
            {
                DiseaseIds.UnionWith(diseaseIds);
                return this;
            }

            public Builder ShouldIncludeArticles()
            {
                IncludeArticles = true;
                return this;
            }

            public Builder ShouldIncludeLocalCaseCount(int geonameId)
            {
                ShouldIncludeLocations();
                GeonameId = geonameId;
                IncludeLocalCaseCount = true;
                return this;
            }

            public Builder ShouldIncludeLocations()
            {
                IncludeLocations = true;
                return this;
            }

            public Builder ShouldIncludeProximalLocations(int geonameId)
            {
                ShouldIncludeLocations();
                GeonameId = geonameId;
                IncludeProximalLocations = true;
                return this;
            }

            public Builder ShouldIncludeLocationsHistory()
            {
                ShouldIncludeLocations();
                IncludeLocationsHistory = true;
                return this;
            }

            public Builder ShouldIncludeExportationRisk()
            {
                IncludeExportationRisk = true;
                return this;
            }

            public Builder ShouldIncludeImportationRisk(int geonameId)
            {
                IncludeImportationRisk = true;
                GeonameId = geonameId;
                return this;
            }

            public Builder ShouldIncludeSourceAirports(SourceAirportConfig airportConfig)
            {
                SourceAirportConfig = airportConfig;
                return this;
            }

            public Builder ShouldIncludeDestinationAirports(AirportConfig airportConfig)
            {
                DestinationAirportConfig = airportConfig;
                // Destination Airports require locations for risk
                ShouldIncludeLocations();
                return this;
            }

            public Builder ShouldIncludeDiseaseInformation()
            {
                IncludeDiseaseInformation = true;
                return this;
            }

            public Builder ShouldIncludeCalculationMetadata()
            {
                IncludeCalculationMetadata = true;
                return this;
            }

            public EventConfig Build()
            {
                return new EventConfig(this);
            }
        }
    }
}