using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppCenterAnalytics.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await NavigateToXamarinHomePage());
            CrashCommand = new Command(() => Crashes.GenerateTestCrash());
        }

        public ICommand CrashCommand { get; }
        public ICommand OpenWebCommand { get; }

        private async Task NavigateToXamarinHomePage()
        {
            base.Logging.LogEvent(AppLogLevel.Info, "Opened Xamarin.com");
            await Browser.OpenAsync("https://xamarin.com");
        }
    }
}