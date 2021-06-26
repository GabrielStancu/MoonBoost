using Data.Models;
using System;
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

        public async Task DeleteActivity(Activity activity)
        {
            await DeleteAsync(activity);
        }
    }
}
