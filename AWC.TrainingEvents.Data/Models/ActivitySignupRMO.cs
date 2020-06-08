using AutoMapper;
using AWC.TrainingEvents.Abstract.IModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AWC.TrainingEvents.Data.Models
{
    /// <summary>
    /// Actvity Signup Relational Mapping Object
    /// </summary>
    public class ActivitySignupRMO : IActivitySignup
    {
        private static readonly IMapper _mapper;

        static ActivitySignupRMO()
        {
            var config = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<IActivitySignup, ActivitySignupRMO>()
                     .ForMember(dest => dest.ActivityRMO, opt => opt.MapFrom(src => src.Activity));
                 cfg.CreateMap<IActivity, ActivityRMO>();
             });
            _mapper = config.CreateMapper();
        }

        public static ActivitySignupRMO NewFrom(IActivitySignup activitySignup)
        {
            var newSignup = _mapper.Map<ActivitySignupRMO>(activitySignup);

            // We're not adding a new activity--it's existing so we don't need to add it to the DB
            // just the relationship via foreign key
            if(activitySignup.Activity.Id != Guid.Empty)
            {
                newSignup.ActivityId = activitySignup.Activity.Id;
                newSignup.ActivityRMO = null;
            }
            return newSignup;
        }

        // RMO implementation details
        public ActivityRMO ActivityRMO { get; set; }
        public Guid ActivityId { get; set; }

        // IActivity Signup Implementation
        public Guid Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public DateTime PrefferedStart { get; set; } 
        public int TimeOfDayMinutes { get; set; } 
        public int YearsExperience { get; set; } 
        public IActivity Activity => ActivityRMO;
        public string Comments { get; set; } 
    }

    /// <summary>
    /// I like keeping the configuration extension for each relational mapping object (RMO) 
    /// in the same file as the object class itself.
    /// </summary>
    public static class ActivitySignupConfig
    {
        public static void ConfigActivitySignup(this ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<ActivitySignupRMO>();
            builder.ToTable("Signups");
            // Id is configured by convention

            // Let's customize Name because we know it has a max and is required
            builder.Property(s => s.FirstName)
                .HasMaxLength(50)
                .IsUnicode() // To support names from various cultures
                .IsRequired()
                .HasColumnName("first_name");
            // SQL Server has a lot of different naming convewntion standards.
            // I'm adopting a more generic SQL naming convention -- kabob case, all lower case

            builder.Property(s => s.LastName)
                .HasMaxLength(50)
                .IsUnicode() // To support names from various cultures
                .IsRequired()
                .HasColumnName("last_name");

             builder.Property(s => s.Email)
                .HasMaxLength(500)
                .IsUnicode()  // Modern email addresses support unicode characters
                .IsRequired()
                .HasColumnName("email");

            builder.Property(s => s.PrefferedStart)
                .IsRequired()
                .HasColumnName("preffered_start");

            builder.Property(s => s.TimeOfDayMinutes)
                // Should never be bigger than 1440, which is too big for tinyint 
                // but small int should cover it 
                .HasColumnType("smallint")
                .IsRequired()
                .HasColumnName("time_of_day_minutes");

            builder.Property(s => s.YearsExperience)
                .HasColumnType("tinyint") // years is never higher than 100
                .IsRequired()
                .HasColumnName("years_experience");

            builder.Property(s => s.Comments)
                .IsRequired(false)
                .HasColumnName("comments");

            // This is just a pointer to the navigational property ActivityRMO
            builder.Ignore(s => s.Activity);

            builder.HasOne(s => s.ActivityRMO)
                .WithMany(a => a.Signups)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(s => s.ActivityId);

        }
    }
}
