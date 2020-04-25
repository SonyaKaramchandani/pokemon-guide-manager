using Microsoft.ApplicationInsights;

namespace Biod.Insights.Service.Configs
{
    public class UserRoleConfig
    {
        public readonly string RoleId;
        public readonly bool IncludePublicRolesOnly;
        public readonly bool IncludePrivateRolesOnly;
        
        private UserRoleConfig(Builder builder)
        {
            RoleId = builder.RoleId;
            IncludePublicRolesOnly = builder.IncludePublicRolesOnly;
            IncludePrivateRolesOnly = builder.IncludePrivateRolesOnly;
        }
        
        public class Builder
        {
            protected internal string RoleId;
            protected internal bool IncludePublicRolesOnly;
            protected internal bool IncludePrivateRolesOnly;

            public Builder SetRoleId(string roleId)
            {
                RoleId = roleId;
                return this;
            }

            public Builder ShouldIncludePublicRolesOnly()
            {
                IncludePublicRolesOnly = true;
                IncludePrivateRolesOnly = false;
                return this;
            }
            
            public Builder ShouldIncludePrivateRolesOnly()
            {
                IncludePrivateRolesOnly = true;
                IncludePublicRolesOnly = false;
                return this;
            }
            
            public UserRoleConfig Build()
            {
                return new UserRoleConfig(this);
            }
        }
    }
}