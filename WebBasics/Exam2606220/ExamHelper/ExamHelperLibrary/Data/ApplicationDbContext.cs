namespace ExamHelperLibrary.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Common;

    public class ApplicationDbContext : DbContext
    {
        // TODO: Add DbSets for database models
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbContextConnectionString.GetConnectionString);
            }
        }

        // TODO: Make relations if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}