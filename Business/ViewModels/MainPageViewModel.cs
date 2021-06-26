using Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Business.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Activity> Activities { get; set; } = new ObservableCollection<Activity>();
        public ObservableCollection<Plan> Plans { get; set; } = new ObservableCollection<Plan>();
        public Activity SelectedActivity { get; set; }
        public Plan SelectedPlan { get; set; }

        public MainPageViewModel()
        {
            Activities.Add(
                new Activity()
                {
                    Completed = true,
                    PlanId = 1,
                    Title = "Walk the dog"
                });
            Activities.Add(
                new Activity()
                {
                    Completed = false,
                    PlanId = 1,
                    Title = "Solve the Maths homework"
                });

            Plans.Add(
                new Plan()
                {
                    Id = 1, 
                    Name = "Daily Plan"
                });

            Plans.Add(
                new Plan()
                {
                    Id = 1,
                    Name = "Secondary Plan"
                });

            SelectedPlan = Plans[0];
        }
    }
}
