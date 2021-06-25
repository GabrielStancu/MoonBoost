using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Xamarin.Forms;

namespace Data.Context
{
    public class PlanContext: DbContext
    {
        public DbSet<Activity> Activity { get; private set; }
        public DbSet<ToDo> ToDo { get; private set; }
        public DbSet<Plan> Plan { get; private set; }

        private const string _databaseName = "planner.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", _databaseName);
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported.");
            }

            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
