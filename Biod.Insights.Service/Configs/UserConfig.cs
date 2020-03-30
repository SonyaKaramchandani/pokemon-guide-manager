namespace Biod.Insights.Service.Configs
{
    public class UserConfig
    {
        public readonly bool TrackChanges;
        public readonly string UserId;
        
        private UserConfig(Builder builder)
        {
            TrackChanges = builder.TrackChanges;
            UserId = builder.UserId;
        }
        
        public class Builder
        {
            protected internal bool TrackChanges;
            protected internal string UserId;

            public Builder SetUserId(string userId)
            {
                UserId = userId;
                return this;
            }

            public Builder ShouldTrackChanges()
            {
                TrackChanges = true;
                return this;
            }
            
            public UserConfig Build()
            {
                return new UserConfig(this);
            }
        }
    }
}