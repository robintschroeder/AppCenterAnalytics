using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppCenterAnalytics.Services
{
    public class AppState : IAppState
    {
        private Guid _installId = Guid.Empty;

        public Guid GetInstallId()
        {
            return _installId;
        }

        public async Task Init()
        {
            if (await CheckAppCenter())
            {
                //https://docs.microsoft.com/en-us/appcenter/sdk/other-apis/xamarin#identify-installations//
                System.Guid? installId = await AppCenter.GetInstallIdAsync();
                if (installId != null) { _installId = (Guid)installId; }
            }
        }

        private async Task<bool> CheckAppCenter()
        {
            var retValue = true; //let's be optimistic
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    //check to make sure analytics is wired up
                    bool isAnalyticsEnabled = await Analytics.IsEnabledAsync();
                    if (!isAnalyticsEnabled)
                    {
                        //we can't really report to appcenter that appcenter isn't working!
                        Debug.WriteLine($"[{this.GetType().ToString()}] Warning: AppCenter Analytics is not enabled.");
                        retValue = false;
                    }

                    bool isCrashEnabled = await Crashes.IsEnabledAsync();
                    if (!isCrashEnabled)
                    {
                        //we can't really report to appcenter that appcenter isn't working!
                        Debug.WriteLine($"[{this.GetType().ToString()}] Warning: AppCenter Crash Reporting is not enabled.");
                        retValue = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //we can't really report to appcenter that appcenter isn't working!
                Debug.WriteLine($"[{this.GetType().ToString()}] Exception while checking if AppCenter is enabled {ex.Message} {ex.StackTrace}");
                retValue = false;
            }
            return retValue;
        }
    }
}