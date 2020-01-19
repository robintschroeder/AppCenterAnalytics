using System;
using System.Collections.Generic;
using System.Text;

namespace AppCenterAnalytics.Services
{
    public interface ILogging
    {
        void LogEvent(AppLogLevel level, string message);

        void LogEvent(AppLogLevel level, string message, Dictionary<string, string> dict);
    }
}