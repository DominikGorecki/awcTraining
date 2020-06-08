using AWC.TrainingEvents.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AWC.TrainingEvents.Data
{

    // We will use this DB context when implementing ALL of our __DataServices. For now we have one, but
    // we can still take advantage of all the EF Core caching and features but still seperating the implementation
    // of multiple Data services... 
    public class ApplicationDbContext
        : DbContext
    {
        public DbSet<ActivitySignupRMO> Signups { get; set; }
        public DbSet<ActivityRMO> Activities { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            // Code-first approach. We can remove this if we want to create our own, more optimized
            // db with T-SQL.
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigActivitySignup();
            modelBuilder.ConfigActivity();
        }
    }
}
