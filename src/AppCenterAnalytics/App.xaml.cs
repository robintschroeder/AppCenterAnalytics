using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCenterAnalytics.Services;
using AppCenterAnalytics.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace AppCenterAnalytics
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //The Xamarin.Forms.DependencyService is a SERVICE LOCATOR and not an IoC CONTAINER
            DependencyService.Register<AppState>();
            //Logging gets AppState in its constructor, so it must be registered after AppState
            DependencyService.Register<Logging>();
            //MockDataStore gets AppState and Logging in its constructor, so it must be registered after AppState and Logging
            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();
        }

        protected override void OnResume()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=a7a2d1ad-07ab-43e2-a9cb-7fb8b7242cc7;" +
                  "uwp={Your UWP App secret here};" +
                  "android=b3f14a4f-f414-4c26-bf1a-d0808076fec7",
                  typeof(Analytics), typeof(Crashes));

            IAppState AppState = DependencyService.Get<IAppState>();
            AppState.Init();

            //initialize the log levels for the app and for what messages are sent to the console
#if DEBUG
            //this will dictate the AppCenter logs sent to the console
            AppState.SetAppCenterConsoleLogLevel(LogLevel.Verbose);
            //this will dictate what log level we will send to AppCenter
            AppState.SetAppLogLevel(AppLogLevel.Verbose);
#else
            //this will dictate the AppCenter logs sent to the console
            AppState.SetAppCenterConsoleLogLevel(LogLevel.Warn);
            //this will dictate what log level we will send to AppCenter
            AppState.SetAppLogLevel(AppLogLevel.Info);
#endif
        }
    }
}