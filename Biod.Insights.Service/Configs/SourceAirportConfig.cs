namespace Biod.Insights.Service.Configs
{
    public class SourceAirportConfig
    {
        public readonly int? EventId;
        public readonly bool IncludeAirportInformation;
        public readonly bool IncludeEventInformation;
        
        private SourceAirportConfig(Builder builder)
        {
            EventId = builder.EventId;
            IncludeAirportInformation = builder.IncludeAirportInformation;
            IncludeEventInformation = builder.IncludeEventInformation;
        }

        public class Builder
        {
            protected internal int? EventId;
            protected internal bool IncludeAirportInformation;
            protected internal bool IncludeEventInformation;
            
            public Builder ShouldIncludeAllProperties()
            {
                return this
                    .ShouldIncludeAirportInformation()
                    .ShouldIncludeEventInformation();
            }

            public Builder SetEventId(int eventId)
            {
                EventId = eventId;
                return this;
            }
            
            public Builder ShouldIncludeAirportInformation()
            {
                IncludeAirportInformation = true;
                return this;
            }

            public Builder ShouldIncludeEventInformation()
            {
                IncludeEventInformation = true;
                return this;
            }
            
            public SourceAirportConfig Build()
            {
                return new SourceAirportConfig(this);
            }
        }
    }
}