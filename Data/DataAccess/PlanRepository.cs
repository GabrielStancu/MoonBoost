using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class PlanRepository: GenericRepository<Plan>
    {
        public async Task<Plan> SelectPlanWithActivities(int planId)
        {
            return await CreateContext()
                .Plan
                .Include(p => p.Activities)
                .FirstOrDefaultAsync(p => p.Id == planId);
        }

        public async Task<Plan> CreatePlan(string name)
        {
            var plan = new Plan()
            {
                Name = name
            };

            await InsertAsync(plan);
            return plan;
        }

        public async Task RenamePlan(Plan plan, string newName)
        {
            plan.Name = newName;
            await UpdateAsync(plan);
        }

        public async Task DeleteActivity(Plan plan)
        {
            await DeleteAsync(plan);
        }
    }
}
