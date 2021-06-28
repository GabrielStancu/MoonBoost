using Data.DataAccess;
using Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        private readonly ActivityRepository _activityRepository;
        private readonly PlanRepository _planRepository;

        public ObservableCollection<Activity> Activities { get; set; } = new ObservableCollection<Activity>();
        public ObservableCollection<Plan> Plans { get; set; } = new ObservableCollection<Plan>();
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
        private Plan _selectedPlan;
        public Plan SelectedPlan 
        { 
            get { return _selectedPlan; }
            set
            {
                _selectedPlan = value;
                SetProperty(ref _selectedPlan, value);
                EnabledNewDayButton = _selectedPlan.PlanDate < DateTime.Today;
            }
        }

        private bool _enabledNewDayButton;
        public bool EnabledNewDayButton
        {
            get { return _enabledNewDayButton; }
            set
            {
                _enabledNewDayButton = value;
                SetProperty<bool>(ref _enabledNewDayButton, value);
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

        public MainPageViewModel(ActivityRepository activityRepository, PlanRepository planRepository)
        {
            _activityRepository = activityRepository;
            _planRepository = planRepository;
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

        public async Task RenameActivityAsync(string newActivityName)
        {
            await _activityRepository.RenameActivity(SelectedActivity, newActivityName);
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
            SelectedActivity = null;
        }

        public async Task StartNewDayAsync()
        {
            foreach (var plan in Plans)
            {
                await _planRepository.UpdatePlanDate(plan);
                var activities = await _activityRepository.CreateDailyActivities(plan);
                plan.Activities = activities.ToList();

                if(plan.Id == SelectedPlan.Id)
                {
                    Activities.Clear();
                    foreach (var activity in plan.Activities)
                    {
                        Activities.Add(activity);
                    }
                }
            }

            SelectedPlan.PlanDate = DateTime.Today;
            EnabledNewDayButton = false;
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
