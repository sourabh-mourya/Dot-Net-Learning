using System.ComponentModel.DataAnnotations;

namespace Bind_Multiple_Models_With_Single_View.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(14, MinimumLength = 3,
            ErrorMessage = "Name must be between 3 and 14 characters.")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select your gender.")]
        [RegularExpression("^(Male|Female|Other)$",
            ErrorMessage = "Please select a valid gender.")]
        public string Gender { get; set; } = string.Empty;


        [Required(ErrorMessage = "Standard is required.")]
        [Range(1, 12, ErrorMessage = "Standard must be between 1 and 12.")]
        public int Standard { get; set; }
    }
}