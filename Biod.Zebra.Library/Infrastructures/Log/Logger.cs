using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Biod.Zebra.Library.Infrastructures.Log
{
    public class Logger : ILogger
    {
        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }

        private readonly ILog _log;

        public static ILogger GetLogger(string name)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsUnitTest")))
            {
                return new MockedLogger();
            }
            return new Logger(name);
        }

        private Logger(string name)
        {
            _log = LogManager.GetLogger(name);
        }

        static Logger()
        {
            if (LogManager.GetRepository().Configured)
            {
                return;
            }

            var loggingEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("isLogToFile"));

            if (loggingEnabled)
            {
                log4net.Config.XmlConfigurator.Configure();
            }

            try
            {
                // Fallback to the default configuration if log4net is not configured
                if (loggingEnabled && !LogManager.GetRepository().Configured)
                {
                    string logFolder = ConfigurationManager.AppSettings["LogFolder"];
                    if (string.IsNullOrWhiteSpace(logFolder) || !Directory.Exists(logFolder))
                    {
                        logFolder = Path.GetTempPath();
                    }

                    string logFile = ConfigurationManager.AppSettings["LogFileName"];
                    if (string.IsNullOrWhiteSpace(logFile))
                    {
                        logFile = Assembly.GetExecutingAssembly().GetName().Name + ".log";
                    }

                    string levelString = ConfigurationManager.AppSettings["LogLevel"];
                    if (string.IsNullOrWhiteSpace(levelString))
                    {
                        levelString = "Info";
                    }

                    // http://stackoverflow.com/questions/16336917/can-you-configure-log4net-in-code-instead-of-using-a-config-file
                    var hierarchy = LogManager.GetRepository() as Hierarchy ?? new Hierarchy();

                    var layout = new PatternLayout
                    {
                        ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
                    };

                    var appender = new RollingFileAppender
                    {
                        File = Path.Combine(logFolder, logFile),
                        Layout = layout,
                        AppendToFile = true,
                        RollingStyle = RollingFileAppender.RollingMode.Size,
                        MaxFileSize = 1000000,
                        MaxSizeRollBackups = 10,
                        StaticLogFileName = true
                    };

                    layout.ActivateOptions();
                    appender.ActivateOptions();
                    hierarchy.Root.AddAppender(appender);

                    log4net.Core.Level level;
                    switch (levelString.ToLower())
                    {
                        case "debug":
                            level = log4net.Core.Level.Debug;
                            break;
                        case "warn":
                        case "warning":
                            level = log4net.Core.Level.Warn;
                            break;
                        case "error":
                            level = log4net.Core.Level.Error;
                            break;
                        case "fatal":
                            level = log4net.Core.Level.Fatal;
                            break;
                        default:
                            level = log4net.Core.Level.Info;
                            break;
                    }

                    hierarchy.Root.Level = level;
                    hierarchy.Configured = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to configure Log. Error: " + ex);
            }
        }

        public static void SetProperty(string name, object value)
        {
            LogicalThreadContext.Properties[name] = value;
        }

        public void Debug(string message, string code = null)
        {
            Log(LogLevel.Debug, message, null, code);
        }

        public void Info(string message, string code = null)
        {
            Log(LogLevel.Info, message, null, code);
        }

        public void Warning(string message, Exception exception = null, string code = null)
        {
            Log(LogLevel.Warning, message, exception, code);
        }

        public void Error(string message, Exception exception = null, string code = null)
        {
            Log(LogLevel.Error, message, exception, code);
        }

        public void Fatal(string message, Exception exception = null, string code = null)
        {
            Log(LogLevel.Fatal, message, exception, code);
        }

        public void SetLogicalThreadProperty(string name, object value)
        {
            LogicalThreadContext.Properties[name] = value;
        }

        public void Log(LogLevel level, string message, Exception exception = null, string code = null)
        {
            try
            {
                if (code != null) SetLogicalThreadProperty("code", code); //set the current log code

                switch (level)
                {
                    case LogLevel.Debug:
                        if (exception != null)
                            _log.Debug(message, exception);
                        else
                            _log.Debug(message);
                        break;

                    case LogLevel.Warning:
                        if (exception != null)
                            _log.Warn(message, exception);
                        else
                            _log.Warn(message);
                        break;

                    case LogLevel.Error:
                        if (exception != null)
                            _log.Error(message, exception);
                        else
                            _log.Error(message);
                        break;
                    case LogLevel.Fatal:
                        if (exception != null)
                            _log.Fatal(message, exception);
                        else
                            _log.Fatal(message);
                        break;
                    case LogLevel.Info:
                        if (exception != null)
                            _log.Info(message, exception);
                        else
                            _log.Info(message);
                        break;
                }
                if (code != null) SetLogicalThreadProperty("code", null);//clears the code
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Logger failed to log, Error: {ex}," +
                    $"Original Message: {message} {(exception != null ? $", Exception: {exception}" : string.Empty)}");
            }
        }
    }
}