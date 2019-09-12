using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Models
{
    static class ConfigVariables
    {
        public static DateTime RetrieveDate
        {
            get { return DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings.Get("retrieveDataByDays"))).Date; }
        }
        public static int CommandTimeout
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings.Get("commandTimeout")); }
        }
    }
}
