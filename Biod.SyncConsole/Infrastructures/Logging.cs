using System;
using System.Configuration;
using System.IO;

namespace Biod.SyncConsole.Infrastructures
{
    class Logging
    {
        public static void Log(string logMessage)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("isLogToFile")))
            {
                string file = ConfigurationManager.AppSettings.Get("logFile") + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                using (StreamWriter w = File.AppendText(file))
                {
                    w.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), logMessage);
                }
            }
        }

        public static void Log(Exception e, string info)
        {
            // check if we can log here
            if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("isLogToFile")))
            {
                if (e.InnerException != null)
                {
                    Log("ERROR " + info
                        + Environment.NewLine + e.Message + Environment.NewLine + e.InnerException.Message);
                }
                else
                {
                    Log("ERROR " + info + Environment.NewLine + e.Message);
                }
            }
        }
    }
}
