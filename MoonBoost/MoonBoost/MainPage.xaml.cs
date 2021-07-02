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

        private void OnCurrentPlanChanged(object sender, EventArgs e)
        {
            _model.LoadPlanActivitiesAsync();
        }

        private async void OnDeleteActivityClicked(object sender, EventArgs e)
        {
            bool? deleteActivity = await DisplayAlert("Deleting activity", "Do you want to proceed deleting this activity?", "Yes", "No");

            if (deleteActivity != null && deleteActivity == true)
            {
                await _model.DeleteActivityAsync();
            }
        }

        private async void OnRenameActivityClicked(object sender, EventArgs e)
        {
            var activityName = await DisplayPromptAsync("Rename activity", "Type new activity name...");
            if(!string.IsNullOrEmpty(activityName))
            {
                await _model.RenameActivityAsync(activityName);
            }       
        }

        private async void OnOptionsClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("What do you want to do?", "Cancel", null,
                "New Day", "New Activity", "Add New Plan", "Delete Current Plan");

            switch (action)
            {
                case "New Day":
                    OnNewDay();
                    break;
                case "New Activity":
                    OnNewActivity();
                    break;
                case "Add New Plan":
                    OnNewPlan();
                    break;
                case "Delete Current Plan":
                    OnDeletePlan();
                    break;
            }
        }
        private async void OnNewDay()
        {
            await _model.StartNewDayAsync();
        }
        private async void OnNewActivity()
        {
            var activityName = await DisplayPromptAsync("New activity", "Type new activity name...");
            if(!string.IsNullOrEmpty(activityName))
            {
                await _model.AddActivityToPlanAsync(activityName);
            }
        }
        private async void OnNewPlan()
        {
            var planName = await DisplayPromptAsync("New plan", "Type new plan name...");
            if(!string.IsNullOrEmpty(planName))
            {
                await _model.AddNewPlanAsync(planName);
            } 
        }
        private async void OnDeletePlan()
        {
            if (_model.SelectedPlan != _model.Plans[0])
            {
                bool? deletePlan = await DisplayAlert("Deleting plan", "Do you want to proceed deleting this plan?", "Yes", "No");

                if (deletePlan != null && deletePlan == true)
                {
                    await _model.DeletePlanAsync();
                }
            }
            else
            {
                await DisplayAlert("Error", "Cannot delete default plan", "OK");
            }
        }   
    }
}
