using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Infrastructures
{
    public class AppSettingProvider : IAppSettingProvider
    {
        public string Get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
