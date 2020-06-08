using AWC.TrainingEvents.Abstract.IModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWC.TrainingEvents.Data.Models
{
    public class ActivityRMO : IActivity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ActivitySignupRMO> Signups { get; set; }
    }

    public static class ActivityConfig
    {
        public static void ConfigActivity(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<ActivityRMO>();
            builder.ToTable("Activity");

            builder.Property(a => a.Name)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("name");

            // One to many relationship setup in ConfigActivitySignup
        }
    }

}
