using Business.ViewModels;
using Data.DataAccess;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoonBoost
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage(new MainPageViewModel(new ActivityRepository(), new PlanRepository()));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
