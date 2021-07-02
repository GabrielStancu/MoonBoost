using Data.DataAccess;
using Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Business.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        private readonly ActivityRepository _activityRepository;
        private readonly PlanRepository _planRepository;

        private ObservableCollection<Activity> _activities;
        public ObservableCollection<Activity> Activities 
        {
            get { return _activities; } 
            set
            {
                _activities = value;
                SetProperty<ObservableCollection<Activity>>(ref _activities, value);
            }
        } 
        public ObservableCollection<Plan> Plans { get; set; } = new ObservableCollection<Plan>();
        private Plan _selectedPlan;
        public Plan SelectedPlan 
        { 
            get { return _selectedPlan; }
            set
            {
                _selectedPlan = value;
                SetProperty(ref _selectedPlan, value);
            }
        }

        private Activity _selectedActivity;
        public Activity SelectedActivity
        {
            get { return _selectedActivity; }
            set 
            {
                _selectedActivity = value;
                SetProperty<Activity>(ref _selectedActivity, value);
            }
        }

        private bool _enabledDeletePlanButton;
        public bool EnabledDeletePlanButton
        {
            get { return _enabledDeletePlanButton; }
            set
            {
                _enabledDeletePlanButton = value;
                SetProperty<bool>(ref _enabledDeletePlanButton, value);
            }
        }

        public Command RenameActivityCommand { get; set; }
        public Command DeleteActivityCommand { get; set; }

        public MainPageViewModel(ActivityRepository activityRepository, PlanRepository planRepository)
        {
            _activityRepository = activityRepository;
            _planRepository = planRepository;
            Activities = new ObservableCollection<Activity>();

            RenameActivityCommand = new Command<Activity>(activity => { SetSelectedActivityCommand(activity); });
            DeleteActivityCommand = new Command<Activity>(activity => { SetSelectedActivityCommand(activity); });
        }

        public async Task LoadPlansAsync()
        {
            var plans = await _planRepository.SelectAllPlansAsync();
            
            Plans.Clear();
            foreach (var plan in plans)
            {
                Plans.Add(plan);
            }

            await SelectCurrentPlanAsync();
        }

        public void LoadPlanActivitiesAsync()
        {
            Activities.Clear();
            foreach (var activity in SelectedPlan.Activities)
            {
                Activities.Add(activity);
            }
            EnabledDeletePlanButton = SelectedPlan.Id != Plans[0].Id;
        }

        public async Task AddActivityToPlanAsync(string activityName)
        {
            var activity = await _activityRepository.CreateActivity(activityName, SelectedPlan.Id);
            Activities.Add(activity);
        }

        public async Task AddNewPlanAsync(string planName)
        {
            var plan = await _planRepository.CreatePlanAsync(planName);
            Plans.Add(plan);
            SelectedPlan = plan;
        }

        public void SetSelectedActivityCommand(Activity activity)
        {
            SelectedActivity = activity;
        }

        public async Task RenameActivityAsync(string name)
        {
            await _activityRepository.RenameActivity(SelectedActivity, name);
        }

        public async Task DeletePlanAsync()
        {
            await _planRepository.DeletePlanAsync(SelectedPlan);
            Plans.Remove(SelectedPlan);
            SelectedPlan = Plans[0];
        }

        public async Task DeleteActivityAsync()
        {
            await _activityRepository.DeleteActivity(SelectedActivity);
            Activities.Remove(SelectedActivity);
        }

        public async Task StartNewDayAsync()
        {
            if(SelectedPlan.PlanDate < DateTime.Today)
            {
                await _planRepository.UpdatePlanDate(SelectedPlan);
                var activities = await _activityRepository.CreateDailyActivities(SelectedPlan);
                SelectedPlan.Activities = activities.ToList();
                Activities.Clear();
                foreach (var activity in SelectedPlan.Activities)
                {
                    Activities.Add(activity);
                }

                SelectedPlan.PlanDate = DateTime.Today;
            }  
        }

        private async Task SelectCurrentPlanAsync()
        {
            if(Plans.Count == 0)
            {
                string planName = "Daily Plan";
                var plan = await _planRepository.CreatePlanAsync(planName);
                Plans.Add(plan);
            }

            SelectedPlan = Plans[0];
        }
    }
}
