using System.ComponentModel.DataAnnotations;

namespace Bind_Multiple_Models_With_Single_View.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Teacher name is required.")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female|Other)$",
            ErrorMessage = "Please select a valid gender.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "Experience must be between 1 and 50 years.")]
        public int Experience { get; set; }

        [Range(10000, 200000, ErrorMessage = "Salary must be between 10000 and 200000.")]
        public decimal Salary { get; set; }
    }
}