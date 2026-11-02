using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace curd_project_using_Code_First_Approach.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("First Name", TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Student name is required")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Name must be between 3 and 100 characters")]
        [Display(Name = "Student Name")]
        public string name { get; set; }

        [Column("Age", TypeName = "int")]
        [Required(ErrorMessage = "Age is required")]
        [Range(5, 100, ErrorMessage = "Age must be between 5 and 100")]
        [Display(Name = "Student Age")]
        public int age { get; set; }

        [Column("Gender", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^(Male|Female|Other)$",
            ErrorMessage = "Gender must be Male, Female or Other")]
        [Display(Name = "Student Gender")]
        public string gender { get; set; }

        [Column("Standard", TypeName = "int")]
        [Required(ErrorMessage = "Standard is required")]
        [Range(1, 12, ErrorMessage = "Standard must be between 1 and 12")]
        [Display(Name = "Student Standard")]
        public int standard { get; set; }
    }
}
