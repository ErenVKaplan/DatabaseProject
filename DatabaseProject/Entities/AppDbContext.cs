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
        public DbSet<Bank> Banks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Bank>()
                .HasOne(b=>b.User)
                .WithMany(u=>u.Banks)
                .HasForeignKey(b=>b.UserId);
        }
    }
}
