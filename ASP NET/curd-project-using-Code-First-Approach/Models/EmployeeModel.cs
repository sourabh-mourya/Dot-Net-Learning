
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace curd_project_using_Code_First_Approach.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Column("First Name", TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Employee name must be between 3 and 100 characters")]
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Employee age is required")]
        [Range(18, 60,
            ErrorMessage = "Employee age must be between 18 and 60")]
        [Display(Name = "Employee Age")]
        public int EmpAge { get; set; }


        [Required(ErrorMessage = "Employee salary is required")]
        [Range(1000, 10000000,
            ErrorMessage = "Salary must be between 1,000 and 1 crore")]
        [Display(Name = "Employee Salary")]
        public int EmpSalary { get; set; }


        [Required(ErrorMessage = "Employee designation is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Designation must be between 2 and 50 characters")]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Employee Designation")]
        public string EmpDesignation { get; set; } = string.Empty;
    }
}
