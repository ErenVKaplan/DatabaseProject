using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        //public DbSet<Example> Examples { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
