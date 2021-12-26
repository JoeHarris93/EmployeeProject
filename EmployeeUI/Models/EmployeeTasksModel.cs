using System.ComponentModel.DataAnnotations;

namespace EmployeeUI.Models
{
    public class EmployeeTasksModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset Deadline { get; set; }
    }
}
