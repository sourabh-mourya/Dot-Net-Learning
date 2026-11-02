using System.ComponentModel.DataAnnotations;

namespace MyASPCoreApp.Models
{
    public class Student
    {
        [Required(ErrorMessage ="Name is required")]
        [StringLength(50,MinimumLength =2)]
        public string Name {get;set;}

        [EmailAddress]
        [Required(ErrorMessage ="Email is required...")]
        public string Email{get;set;}

        [Range(18,60)]
        public int Age{get;set;}

        [Required]
        [RegularExpression(@"^[6-9]\d{9}$",ErrorMessage ="Enter Valid Phone Number")]
        public string Phone{get;set;}
    }
}