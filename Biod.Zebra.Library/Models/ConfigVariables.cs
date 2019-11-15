using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Models
{
    public static class ConfigVariables
    {
        public static DateTime RetrieveDate => DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings.Get("retrieveDataByDays"))).Date;
        public static DateTime CleanupDays => DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings.Get("cleanupDays"))).Date;
    }
}
