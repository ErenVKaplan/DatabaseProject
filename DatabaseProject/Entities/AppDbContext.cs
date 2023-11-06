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
        public DbSet<Adrress> Adrresses { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
               .HasOne(u => u.User)
               .WithMany(a => a.Orders)
               .HasForeignKey(u => u.UserId)
               .OnDelete(DeleteBehavior.Cascade);
           
            modelBuilder.Entity<Order>().
              HasOne(u => u.Adrress)
              .WithMany(a => a.Orders)
              .HasForeignKey(u => u.AdrresId);

            modelBuilder.Entity<Bank>()
                .HasOne(b=>b.User)
                .WithMany(u=>u.Banks)
                .HasForeignKey(b=>b.UserId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Adrress>().
                HasOne(u => u.User)
                .WithMany(a => a.Adrresses)
                .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.NoAction);
           
           


            
        }
    }
}
