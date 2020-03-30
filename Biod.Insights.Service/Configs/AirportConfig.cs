namespace Biod.Insights.Service.Configs
{
    public class AirportConfig
    {
        public readonly int EventId;
        public readonly bool IncludeCity;

        private AirportConfig(Builder builder)
        {
            EventId = builder.EventId;
            IncludeCity = builder.IncludeCity;
        }

        public class Builder
        {
            protected internal readonly int EventId;
            protected internal bool IncludeCity;

            public Builder(int eventId)
            {
                EventId = eventId;
            }

            public Builder ShouldIncludeCity()
            {
                IncludeCity = true;
                return this;
            }

            public AirportConfig Build()
            {
                return new AirportConfig(this);
            }
        }
    }
}