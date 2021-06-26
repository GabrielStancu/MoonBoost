using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Activity: BaseModel
    {
        public string Title { get; set; }
        [Column("Plan_Id")]
        public int PlanId { get; set; }
        public DateTime Date { get; set; }
        public bool Completed { get; set; }
    }
}
