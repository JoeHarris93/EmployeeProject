using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI
{
    /// <summary>
    /// run the next line in the debeloper powershell to create the database and tables in local sql server
    /// dotnet ef database update --project EmployeeAPI
    /// </summary>
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
    }

    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset HireDate { get; set; }

        public List<EmployeeTask> Tasks { get; set; }
    }

    public class EmployeeTask
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FK_EmployeeId")]
        public int? EmployeeId { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset Deadline { get; set; }
    }
}
