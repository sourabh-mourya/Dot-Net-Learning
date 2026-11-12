using JwtAuthenticationAuthorization.Entites;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationAuthorization.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options){}
        public DbSet<User> Users{get;set;}
    }
}