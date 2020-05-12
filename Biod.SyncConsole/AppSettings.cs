using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.SyncConsole
{
    public class Sites : ConfigurationElementCollection
    {
        public Site this[int index]
        {
            get
            {
                return base.BaseGet(index) as Site;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new Site this[string key]
        {
            get { return (Site)BaseGet(key); }
            set
            {
                if (BaseGet(key) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));
                }
                BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new Site();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((Site)element).DiseaseId + ((Site)element).LocationId + ((Site)element).Source;
        }
    }
    public class Site : ConfigurationElement
    {
        [ConfigurationProperty("diseaseId", IsRequired = true)]
        public string DiseaseId
        {
            get
            {
                return this["diseaseId"] as string;
            }
        }
        [ConfigurationProperty("locationId", IsRequired = true)]
        public string LocationId
        {
            get
            {
                return this["locationId"] as string;
            }
        }
        [ConfigurationProperty("source", IsRequired = true)]
        public string Source
        {
            get
            {
                return this["source"] as string;
            }
        }
        [ConfigurationProperty("includeChildren", IsRequired = true)]
        public string IncludeChildren
        {
            get
            {
                return this["includeChildren"] as string;
            }
        }
    }

    public class AutoSurveillanceConfig : ConfigurationSection
    {
        public static AutoSurveillanceConfig GetConfig()
        {
            return (AutoSurveillanceConfig)System.Configuration.ConfigurationManager.GetSection("AutoSurveillance") ?? new AutoSurveillanceConfig();
        }

        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public string BaseUrl
        {
            get
            {
                return this["baseUrl"] as string;
            }
        }
        [ConfigurationProperty("userName", IsRequired = true)]
        public string UserName
        {
            get
            {
                return this["userName"] as string;
            }
        }
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get
            {
                return this["password"] as string;
            }
        }

        [System.Configuration.ConfigurationProperty("Sites")]
        [ConfigurationCollection(typeof(Sites), AddItemName = "Site")]
        public Sites Sites
        {
            get
            {
                object o = this["Sites"];
                return o as Sites;
            }
        }
    }
}

