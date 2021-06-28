using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Plan: BaseModel
    {
        public string Name { get; set; }
        public List<Activity> Activities { get; set; }
        public DateTime PlanDate { get; set; }
    }
}
