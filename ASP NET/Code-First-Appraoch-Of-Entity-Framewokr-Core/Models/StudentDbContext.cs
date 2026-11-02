

using Microsoft.EntityFrameworkCore;

namespace Code_First_Appraoch_Of_Entity_Framewokr_Core.Models
{
    public class StudentDBContext : DbContext
    {
        //mene DbContext class se kyun inherit krwaya hi 
        //yhi DbContext class apnie database ur tables ke saath interact krti hi 
        //without these classs apne Code-first approach useed nhi kr skte hi 

        public StudentDBContext(DbContextOptions options):base(options)
        {
            
        }

        //ab DbSet banauga yhi apnei database me table ko ko represent krega'
        public DbSet<Student> Students{get;set;}
    }
}