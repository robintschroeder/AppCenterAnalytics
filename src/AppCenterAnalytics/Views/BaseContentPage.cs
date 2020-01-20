using AppCenterAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCenterAnalytics.Views
{
    public class BaseContentPage : Xamarin.Forms.ContentPage
    {
        private ILogging _logging;

        public BaseContentPage()
        {
            _logging = DependencyService.Get<ILogging>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if you use nested folders to help id your views, use: this.GetType().FullName
            _logging.LogEvent(AppLogLevel.Info, $"{this.GetType().Name} Appeared");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //if you use nested folders to help id your views, use: this.GetType().FullName
            _logging.LogEvent(AppLogLevel.Info, $"{this.GetType().Name} Disappeared");
        }
    }
}