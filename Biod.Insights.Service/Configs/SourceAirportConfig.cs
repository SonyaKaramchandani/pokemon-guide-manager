namespace Biod.Insights.Service.Configs
{
    public class SourceAirportConfig : AirportConfig
    {
        public readonly bool IncludePopulation;
        public readonly bool IncludeCaseCounts;

        private SourceAirportConfig(Builder builder) : base(builder)
        {
            IncludePopulation = builder.IncludePopulation;
            IncludeCaseCounts = builder.IncludeCaseCounts;
        }

        public new class Builder : AirportConfig.Builder
        {
            protected internal bool IncludePopulation;
            protected internal bool IncludeCaseCounts;

            public Builder(int eventId) : base(eventId)
            {
            }

            public new Builder ShouldIncludeCity()
            {
                base.ShouldIncludeCity();
                return this;
            }

            public new Builder ShouldIncludeImportationRisk(int? geonameId)
            {
                base.ShouldIncludeImportationRisk(geonameId);
                return this;
            }

            public new Builder ShouldIncludeExportationRisk()
            {
                base.ShouldIncludeExportationRisk();
                return this;
            }

            public Builder ShouldIncludePopulation()
            {
                IncludePopulation = true;
                return this;
            }

            public Builder ShouldIncludeCaseCounts()
            {
                IncludeCaseCounts = true;
                return this;
            }

            public new SourceAirportConfig Build()
            {
                return new SourceAirportConfig(this);
            }
        }
    }
}