using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_First_Appraoch_Of_Entity_Framewokr_Core.Models
{
    public class Student
    {
        [Key]//ye database jb  table create krega to isko wo primary key bana dega 
        public int Id { get; set; }
        [Column("Student Name", TypeName = "varchar(100)")]//jb database me table generate hoga to wo Name Column se generate nhi hoga wo 
        //Student Name se generate hoga
        public string Name { get; set; }

        [Column("Student Gender", TypeName = "varchar(20)")]
        public string Gender { get; set; }
        public int Age { get; set; }

        public int Standard {get;set;}
    }
}