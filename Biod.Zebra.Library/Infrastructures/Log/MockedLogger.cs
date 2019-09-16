using System;

namespace Biod.Zebra.Library.Infrastructures.Log
{
    public class MockedLogger : ILogger
    {
        public void Debug(string message, string code = null)
        {
            // Do nothing
        }

        public void Error(string message, Exception exception = null, string code = null)
        {
            // Do nothing
        }

        public void Fatal(string message, Exception exception = null, string code = null)
        {
            // Do nothing
        }

        public void Info(string message, string code = null)
        {
            // Do nothing
        }

        public void Log(Logger.LogLevel level, string message, Exception exception = null, string code = null)
        {
            // Do nothing
        }

        public void SetLogicalThreadProperty(string name, object value)
        {
            // Do nothing
        }

        public void Warning(string message, Exception exception = null, string code = null)
        {
            // Do nothing
        }
    }
}
