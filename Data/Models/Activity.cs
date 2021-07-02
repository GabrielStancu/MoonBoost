using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Activity: BaseModel
    {
        private string _title;
        public string Title 
        {
            get { return _title; } 
            set
            {
                _title = value;
                SetProperty<string>(ref _title, value);
            }
        }
        [Column("Plan_Id")]
        public int PlanId { get; set; }
        public DateTime Date { get; set; }
        public bool Completed { get; set; }
    }
}
