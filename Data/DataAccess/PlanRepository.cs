using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class PlanRepository: GenericRepository<Plan>
    {
        public async Task<IEnumerable<Plan>> SelectAllPlansAsync()
        {
            var plans = await CreateContext()
                .Plan
                .Include(p => p.Activities)
                .ToListAsync();

            foreach (var plan in plans)
            {
                plan.Activities = plan.Activities.Where(a => a.Date == plan.PlanDate).ToList();
            }

            return plans;
        }

        public async Task<Plan> SelectPlanWithActivitiesAsync(int planId)
        {
            var plan = await CreateContext()
                .Plan
                .Include(p => p.Activities)
                .FirstOrDefaultAsync(p => p.Id == planId);
            plan.Activities = plan.Activities.Where(a => a.Date == plan.PlanDate).ToList();

            return plan;
        }

        public async Task<Plan> CreatePlanAsync(string name)
        {
            var plan = new Plan()
            {
                Name = name,
                PlanDate = DateTime.Today
            };

            await InsertAsync(plan);
            return plan;
        }

        public async Task RenamePlanAsync(Plan plan, string newName)
        {
            plan.Name = newName;
            await UpdateAsync(plan);
        }

        public async Task DeletePlanAsync(Plan plan)
        {
            await DeleteAsync(plan);
        }

        public async Task UpdatePlanDate(Plan plan)
        {
            plan.PlanDate = DateTime.Today;
            await UpdateAsync(plan);
        }
    }
}
