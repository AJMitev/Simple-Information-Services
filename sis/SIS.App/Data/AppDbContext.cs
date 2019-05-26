namespace SIS.App.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SISDB;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(k => k.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}