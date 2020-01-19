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

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<AppState>();

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
        }
    }
}