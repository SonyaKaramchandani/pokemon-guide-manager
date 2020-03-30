using System.Collections.Generic;

namespace Biod.Insights.Service.Configs
{
    public class EventConfig
    {
        public readonly int? EventId;

        public readonly int? GeonameId;
        public readonly HashSet<int> DiseaseIds;

        public readonly bool IncludeArticles;
        public readonly bool IncludeLocations;
        public readonly bool IncludeLocationsHistory;
        public readonly bool IncludeExportationRisk;
        public readonly bool IncludeImportationRisk;
        public readonly AirportConfig SourceAirportConfig;
        public readonly bool IncludeSourceAirports;
        public readonly AirportConfig DestinationAirportConfig;
        public readonly bool IncludeDestinationAirports;
        public readonly bool IncludeDiseaseInformation;
        public readonly bool IncludeOutbreakPotential;

        private EventConfig(Builder builder)
        {
            EventId = builder.EventId;
            GeonameId = builder.GeonameId;
            DiseaseIds = builder.DiseaseIds;
            IncludeArticles = builder.IncludeArticles;
            IncludeLocations = builder.IncludeLocations;
            IncludeLocationsHistory = builder.IncludeLocationsHistory;
            IncludeExportationRisk = builder.IncludeExportationRisk;
            IncludeImportationRisk = builder.IncludeImportationRisk;
            SourceAirportConfig = builder.SourceAirportConfig;
            IncludeSourceAirports = builder.SourceAirportConfig != null;
            DestinationAirportConfig = builder.DestinationAirportConfig;
            IncludeDestinationAirports = builder.DestinationAirportConfig != null;
            IncludeDiseaseInformation = builder.IncludeDiseaseInformation;
            IncludeOutbreakPotential = builder.GeonameId.HasValue;
        }


        public class Builder
        {
            protected internal int? EventId;
            protected internal int? GeonameId;
            protected internal readonly HashSet<int> DiseaseIds = new HashSet<int>();
            protected internal bool IncludeArticles;
            protected internal bool IncludeLocations;
            protected internal bool IncludeLocationsHistory;
            protected internal bool IncludeExportationRisk;
            protected internal bool IncludeImportationRisk;
            protected internal AirportConfig SourceAirportConfig;
            protected internal AirportConfig DestinationAirportConfig;
            protected internal bool IncludeDiseaseInformation;

            public Builder()
            {
            }

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

            public Builder ShouldIncludeLocations()
            {
                IncludeLocations = true;
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

            public Builder ShouldIncludeSourceAirports(AirportConfig airportConfig)
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

            public EventConfig Build()
            {
                return new EventConfig(this);
            }
        }
    }
}