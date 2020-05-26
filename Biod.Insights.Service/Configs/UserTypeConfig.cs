using System;

namespace Biod.Insights.Service.Configs
{
    public class UserTypeConfig
    {
        public readonly Guid? UserTypeId;

        private UserTypeConfig(Builder builder)
        {
            UserTypeId = builder.UserTypeId;
        }

        public class Builder
        {
            protected internal Guid? UserTypeId;

            public Builder SetUserTypeId(Guid? userTypeId)
            {
                UserTypeId = userTypeId;
                return this;
            }

            public UserTypeConfig Build()
            {
                return new UserTypeConfig(this);
            }
        }
    }
}