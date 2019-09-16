using System;
using System.IO;

namespace Biod.Zebra.Library.Infrastructures.Log
{
    /// <summary>
    /// File based logger.
    /// 
    /// The name of the file will be the date and time of creation.
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;

        public FileLogger(string directoryPath)
        {
            directoryPath = string.IsNullOrWhiteSpace(directoryPath) ? Path.GetTempPath() : directoryPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            _logFilePath = Path.Combine(directoryPath, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".log");
        }

        public void Debug(string message, string code = null)
        {
            Log(Logger.LogLevel.Debug, message, null, code);
        }

        public void Info(string message, string code = null)
        {
            Log(Logger.LogLevel.Info, message, null, code);
        }

        public void Warning(string message, Exception exception = null, string code = null)
        {
            Log(Logger.LogLevel.Warning, message, null, code);
            if (exception != null)
            {
                Log(Logger.LogLevel.Error, exception.ToString());
            }
        }

        public void Error(string message, Exception exception = null, string code = null)
        {
            Log(Logger.LogLevel.Error, message, null, code);
            if (exception != null)
            {
                Log(Logger.LogLevel.Error, exception.ToString());
            }
        }

        public void Fatal(string message, Exception exception = null, string code = null)
        {
            Log(Logger.LogLevel.Debug, message, null, code);
            if (exception != null)
            {
                Log(Logger.LogLevel.Error, exception.ToString());
            }
        }

        public void Log(Logger.LogLevel level, string message, Exception exception = null, string code = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                string levelString = "";
                switch (level)
                {
                    case Logger.LogLevel.Debug:
                        levelString = "DEBUG";
                        break;
                    case Logger.LogLevel.Info:
                        levelString = "INFO";
                        break;
                    case Logger.LogLevel.Warning:
                        levelString = "WARNING";
                        break;
                    case Logger.LogLevel.Error:
                        levelString = "ERROR";
                        break;
                    case Logger.LogLevel.Fatal:
                        levelString = "FATAL";
                        break;
                }

                File.AppendAllText(_logFilePath, $"[{ DateTime.Now.ToString("HH:mm:ss") }] [{ levelString }] { message }\r\n");
            }
        }

        public void SetLogicalThreadProperty(string name, object value)
        {
            // Do Nothing
        }
    }
}
