namespace Biod.Insights.Service.Configs
{
    public class AirportConfig
    {
        public readonly int EventId;
        public readonly bool IncludeCity;
        public readonly bool IncludeImportationRisk;
        public readonly bool IncludeExportationRisk;
        public readonly int? GeonameId;

        private AirportConfig(Builder builder)
        {
            EventId = builder.EventId;
            IncludeCity = builder.IncludeCity;
            IncludeImportationRisk = builder.IncludeImportationRisk;
            IncludeExportationRisk = builder.IncludeExportationRisk;
            GeonameId = builder.GeonameId;
        }

        public class Builder
        {
            protected internal readonly int EventId;
            protected internal bool IncludeCity;
            protected internal bool IncludeImportationRisk;
            protected internal bool IncludeExportationRisk;
            protected internal int? GeonameId;

            public Builder(int eventId)
            {
                EventId = eventId;
            }

            public Builder ShouldIncludeCity()
            {
                IncludeCity = true;
                return this;
            }

            public Builder ShouldIncludeImportationRisk(int? geonameId)
            {
                GeonameId = geonameId;
                IncludeImportationRisk = true;
                return this;
            }

            public Builder ShouldIncludeExportationRisk()
            {
                IncludeExportationRisk = true;
                return this;
            }

            public AirportConfig Build()
            {
                return new AirportConfig(this);
            }
        }
    }
}