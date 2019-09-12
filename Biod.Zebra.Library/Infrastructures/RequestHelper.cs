using Biod.Zebra.Library.EntityModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biod.Zebra.Library.Infrastructures
{
    public class RequestHelper
    {
        public static void LogClientRequestInfo(HttpRequestBase request)
        {
            try
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsLogRequestInfoEnabled")))
                    return;

                var userHostAddress = request.UserHostAddress;

                // Attempt to parse.  If it fails, we catch below and return "0.0.0.0"
                // Could use TryParse instead, but I wanted to catch all exceptions
                IPAddress.Parse(userHostAddress);

                var xForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(xForwardedFor))
                {
                    // Get a list of public ip addresses in the X_FORWARDED_FOR variable
                    var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IsPrivateIpAddress(ip)).ToList();
                    //Logging.Log(xForwardedFor);
                    // If we found any, return the last one, otherwise return the user host address
                    LogRequestIP(publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress, false);
                }
            }
            catch (Exception)
            {
                // Always return all zeroes for any failure (my calling code expects it)
                LogRequestIP("0.0.0.0", true);
            }
        }

        private static void LogRequestIP(string RequestIP, bool IsPrivateIpAddress)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
            zebraDbContext.usp_SetZebraRequestInfo(RequestIP, IsPrivateIpAddress);
        }


        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }
    }
}
