using Microsoft.AppCenter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCenterAnalytics.Services
{
    public interface IAppState
    {
        AppLogLevel GetAppLogLevel();

        Guid GetInstallId();

        Task Init();

        void SetAppCenterConsoleLogLevel(Microsoft.AppCenter.LogLevel newLogLevel);

        void SetAppLogLevel(AppLogLevel level);
    }
}