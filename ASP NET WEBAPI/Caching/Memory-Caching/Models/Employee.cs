using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }

        public int WorkingYears { get; set; }

        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}