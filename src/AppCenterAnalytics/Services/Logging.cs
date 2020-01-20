using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppCenterAnalytics.Services.AppState))]

namespace AppCenterAnalytics.Services
{
    public class Logging : ILogging
    {
        private IAppState _appState;

        //The Xamarin.Forms.DependencyService is a SERVICE LOCATOR and not an IoC CONTAINER
        //if it was an IoC container, we could inject these into the constructor like this:
        //public Logging(IAppState appState)...
        public Logging()
        {
            _appState = DependencyService.Get<IAppState>();
        }

        public void LogEvent(AppLogLevel level, string message)
        {
            if (_appState.GetAppLogLevel() <= level && !IsOnTestCloud()) //&& !IsEmulatorOrSimulator())
            {
                //include the installId with the message
                var dict = new Dictionary<string, string> { { "InstallId", _appState.GetInstallId().ToString() } };
                Analytics.TrackEvent($"{level}: {message}", dict);
            }
        }

        public void LogEvent(AppLogLevel level, string message, Dictionary<string, string> dict)
        {
            if (_appState.GetAppLogLevel() <= level && !IsOnTestCloud()) //&& !IsEmulatorOrSimulator())
            {
                //include the installId with the message
                dict.Add("InstallId", _appState.GetInstallId().ToString());
                Analytics.TrackEvent($"{level}: {message}", dict);
            }
        }

        private bool IsEmulatorOrSimulator()
        {
            //Thank you, Xamarin.Essentials!
            //This used to be a really big pain, now it is a one-liner!
            return DeviceInfo.DeviceType == DeviceType.Virtual;
        }

        private bool IsOnTestCloud()
        {
            //Xamarin Test Cloud (App Center Test Cloud) is in Denmark... including these stats could give you some unexpected results.

            var isInTestCloud = Environment.GetEnvironmentVariable("XAMARIN_TEST_CLOUD");

            return isInTestCloud != null && isInTestCloud.Equals("1");
        }
    }
}