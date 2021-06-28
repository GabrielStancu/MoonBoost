using Business.ViewModels;
using System;
using Xamarin.Forms;

namespace MoonBoost
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _model;
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            _model = mainPageViewModel;
            BindingContext = _model;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _model.LoadPlansAsync();
            _model.LoadPlanActivitiesAsync();
        }

        private async void OnNewActivityClicked(object sender, EventArgs e)
        {
            var activityName = await DisplayPromptAsync("New activity", "Type new activity name...");
            await _model.AddActivityToPlanAsync(activityName);
        }

        private async void OnNewPlanClicked(object sender, EventArgs e)
        {
            var planName = await DisplayPromptAsync("New plan", "Type new plan name...");
            await _model.AddNewPlanAsync(planName);
        }

        private void OnCurrentPlanChanged(object sender, EventArgs e)
        {
            _model.LoadPlanActivitiesAsync();
        }

        private async void OnActivitySelected(object sender, SelectedItemChangedEventArgs e)
        {
            var activityName = await DisplayPromptAsync("Rename activity", "Type new activity name...");
            await _model.AddActivityToPlanAsync(activityName);
        }

        private async void OnDeletePlanClicked(object sender, EventArgs e)
        {
            if (_model.SelectedPlan != _model.Plans[0])
            {
                bool? deletePlan = await DisplayAlert("Deleting plan", "Do you want to proceed deleting this plan?", "Yes", "No");

                if (deletePlan != null && deletePlan == true)
                {
                    await _model.DeletePlanAsync();
                }
            }
        }

        private async void OnDeleteActivityClicked(object sender, EventArgs e)
        {
            bool? deleteActivity = await DisplayAlert("Deleting activity", "Do you want to proceed deleting this activity?", "Yes", "No");

            if (deleteActivity != null && deleteActivity == true)
            {
                await _model.DeleteActivityAsync();
            }
        }

        private async void OnNewDayClicked(object sender, EventArgs e)
        {
            await _model.StartNewDayAsync();
        }

        
    }
}
