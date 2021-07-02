using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Plan: BaseModel
    {
        public string Name { get; set; }
        public List<Activity> Activities { get; set; }
        private DateTime _planDate;
        public DateTime PlanDate 
        { 
            get { return _planDate; } 
            set
            {
                _planDate = value;
                SetProperty<DateTime>(ref _planDate, value);
            }
        }
    }
}
