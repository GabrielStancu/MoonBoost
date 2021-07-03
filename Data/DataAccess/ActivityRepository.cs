using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class ActivityRepository: GenericRepository<Activity>
    {
        public async Task<Activity> CreateActivity(string title, int planId)
        {
            var activity = new Activity()
            {
                Date = DateTime.Today,
                PlanId = planId,
                Title = title,
                Completed = false
            };

            await InsertAsync(activity);

            return activity;
        }

        public async Task RenameActivity(Activity activity, string newTitle)
        {
            activity.Title = newTitle;
            await UpdateAsync(activity);
        }

        public async Task UpdateActivityStatusAsync(Activity activity)
        {
            await UpdateAsync(activity);
        }

        public async Task DeleteActivity(Activity activity)
        {
            var activities = await CreateContext()
                .Activity
                .Where(a => a.Title == activity.Title)
                .ToListAsync();

            foreach (var a in activities)
            {
                await DeleteAsync(a);
            }
        }

        public async Task<IEnumerable<Activity>> CreateDailyActivities(Plan plan)
        {
            var activities = new List<Activity>();
            foreach (var activity in plan.Activities)
            {
                var todayActivity = await CreateActivity(activity.Title, plan.Id);
                activities.Add(todayActivity);
            }

            return activities;
        }
    }
}
