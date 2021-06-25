using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class ToDo: BaseModel
    {
        [Column("Activity_Id")]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        [Column("Plan_Id")]
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public DateTime Date { get; set; }
        public bool Completed { get; set; }
    }
}
