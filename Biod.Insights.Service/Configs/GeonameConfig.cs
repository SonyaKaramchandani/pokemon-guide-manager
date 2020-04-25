using System;
using System.Collections.Generic;
using System.Linq;

namespace Biod.Insights.Service.Configs
{
    public class GeonameConfig
    {
        public readonly HashSet<int> GeonameIds;
        public readonly bool IncludeShape;
        
        private GeonameConfig(Builder builder)
        {
            GeonameIds = builder.GeonameIds;
            IncludeShape = builder.IncludeShape;
        }
        
        public int GetGeonameId()
        {
            if (GeonameIds.Count != 1)
            {
                throw new InvalidOperationException("There are more than 1 geoname ids in this configuration. This is for retrieving a single geoname id");
            }

            return GeonameIds.Single();
        }

        public class Builder
        {
            protected internal readonly HashSet<int> GeonameIds = new HashSet<int>();
            protected internal bool IncludeShape;

            public Builder AddGeonameId(int geonameId)
            {
                GeonameIds.Add(geonameId);
                return this;
            }
            
            public Builder AddGeonameIds(IEnumerable<int> geonameIds)
            {
                GeonameIds.UnionWith(geonameIds);
                return this;
            }
            
            public Builder ShouldIncludeShape()
            {
                IncludeShape = true;
                return this;
            }
            
            public GeonameConfig Build()
            {
                return new GeonameConfig(this);
            }
        }
    }
}