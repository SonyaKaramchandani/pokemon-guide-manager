using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Biod.Zebra.Api.Core
{
    /// <summary>
    /// Main program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds the web host.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseSetting("detailedErrors", "true")
              .UseIISIntegration()
              .UseStartup<Startup>()
              .CaptureStartupErrors(true)
              .Build();
    }
}
