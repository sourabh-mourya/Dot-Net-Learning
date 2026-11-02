
using Microsoft.EntityFrameworkCore;

namespace curd_project_using_Code_First_Approach.Models
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Student> StudentTable{get;set;}
        public DbSet<Employee> EmployeeTable{get;set;}
    };
    
}