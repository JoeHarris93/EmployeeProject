using System.ComponentModel.DataAnnotations;

namespace EmployeeUI.Models
{
    public class EmployeesModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset HireDate { get; set; }

        public List<EmployeeTasksModel> Tasks { get; set; }
    }
}
