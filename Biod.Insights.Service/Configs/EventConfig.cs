namespace Biod.Insights.Service.Configs
{
    public class EventConfig
    {
        private EventConfig(Builder builder)
        {
            
        }

        public class Builder
        {
            protected internal bool _includeArticles;

            public Builder()
            {
            }

            public Builder includeArticles()
            {
                _includeArticles = true;
                return this;
            }

            public EventConfig Build()
            {
                return new EventConfig(this);
            }
        }
    }
}