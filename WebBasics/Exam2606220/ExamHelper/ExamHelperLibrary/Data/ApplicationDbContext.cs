namespace ExamHelperLibrary.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Common;
    using SharedTrip.Models;

    public class ApplicationDbContext : DbContext
    {
        // TODO: Add DbSets for database models
        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }

        public ApplicationDbContext()
        {

        }

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
            modelBuilder.Entity<UserTrip>()
                .HasKey(x => new {x.UserId, x.TripId});
        }
    }
}