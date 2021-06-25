using Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Business.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<ToDo> ToDos { get; set; } = new ObservableCollection<ToDo>();
        public ObservableCollection<Plan> Plans { get; set; } = new ObservableCollection<Plan>();
        public ToDo SelectedToDo { get; set; }
        public Plan SelectedPlan { get; set; }

        public MainPageViewModel()
        {
            ToDos.Add(
                new ToDo()
                {
                    ActivityId = 1,
                    Completed = true,
                    PlanId = 1,
                    Activity = new Activity()
                    {
                        Id = 1,
                        Title = "Walk the dog"
                    }
                });
            ToDos.Add(
                new ToDo()
                {
                    ActivityId = 2,
                    Completed = false,
                    PlanId = 1,
                    Activity = new Activity()
                    {
                        Id = 2,
                        Title = "Solve the Maths homework"
                    }
                }
                );

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
