using System;
using static Biod.Zebra.Library.Infrastructures.Log.Logger;

namespace Biod.Zebra.Library.Infrastructures.Log
{
    public interface ILogger
    {
        void Debug(string message, string code = null);

        void Info(string message, string code = null);

        void Warning(string message, Exception exception = null, string code = null);

        void Error(string message, Exception exception = null, string code = null);

        void Fatal(string message, Exception exception = null, string code = null);

        void Log(LogLevel level, string message, Exception exception = null, string code = null);

        void SetLogicalThreadProperty(string name, object value);
    }
}
