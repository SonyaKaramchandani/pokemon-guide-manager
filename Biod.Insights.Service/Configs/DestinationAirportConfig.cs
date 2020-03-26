namespace Biod.Insights.Service.Configs
{
    public class DestinationAirportConfig
    {
        public readonly int? EventId;
        
        private DestinationAirportConfig(Builder builder)
        {
            EventId = builder.EventId;
        }

        public class Builder
        {
            protected internal int? EventId;

            public Builder SetEventId(int eventId)
            {
                EventId = eventId;
                return this;
            }
            
            public DestinationAirportConfig Build()
            {
                return new DestinationAirportConfig(this);
            }
        }
    }
}