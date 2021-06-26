using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class GenericRepository<T> where T:BaseModel
    {
        protected PlanContext CreateContext()
        {
            var planContext = (PlanContext)Activator.CreateInstance(typeof(PlanContext));
            planContext.Database.EnsureCreated();
            planContext.Database.Migrate();

            return planContext;
        }

        protected async Task<T> SelectByIdAsync(int id)
        {
            using (var context = CreateContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        protected async Task<List<T>> SelectAllAsync()
        {
            using (var context = CreateContext())
            {
                return await context.Set<T>().ToListAsync();
            }
        }

        protected async Task InsertAsync(T entity)
        {
            using (var context = CreateContext())
            {
                bool alreadyStored = context.Set<T>().Any(e => e.Equals(entity));

                if (!alreadyStored)
                {
                    await context.Set<T>().AddAsync(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected async Task UpdateAsync(T entity)
        {
            using (var context = CreateContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
            }
        }

        protected async Task DeleteAsync(T entity)
        {
            using (var context = CreateContext())
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
