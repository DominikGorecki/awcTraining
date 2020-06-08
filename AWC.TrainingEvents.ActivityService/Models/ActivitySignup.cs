using AWC.TrainingEvents.Abstract.IModels;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;


namespace AWC.TrainingEvents.ActivityService.Models
{
    /// <summary>
    /// The ActivitySignup model is where most of the business logic lives--mostly validation of input.
    /// For this implementation we use a bunch of magic strings for user friendly validation messages
    /// just in case they haven't been caught by the front-end (we don't want to rely on the client of
    /// this service only). However, we can easily modify this implementation so that the user friendly
    /// error messages come fromt he DB or a resource file.
    /// </summary>
    internal class ActivitySignup : IActivitySignup
    {
        private readonly MailAddress _email;
        
        public ActivitySignup(IActivitySignup signup)
        {
            // For now the only time we need to intialize ActivitySignup model is when we create a new signup 
            // -- this implementation detail might change in the future if we have a modify feature. 
            if (signup.Id != Guid.Empty) throw new ArgumentException("Should be a new signup");


            Errors = new List<string>();

            if (TryValidateName(signup.FirstName, out var valFirstName))
                FirstName = valFirstName;
            else Errors.Add("First name needs to be entered and not bigger than 50 chars");

            if (TryValidateName(signup.LastName, out var valLastName))
                LastName = valLastName;
            else Errors.Add("Last name needs to be entered and not bigger than 50 chars");

            SetTimeOfDay(signup.TimeOfDayMinutes);
            SetPrefferedStart(signup.PrefferedStart);
            SetYearsExpereince(signup.YearsExperience);

            // Normally I avoid using try-catch for expected exceptions, BUT this saves a lot of code implementaiton details.
            try
            {
                _email = new MailAddress(signup.Email);
            }
            catch 
            {
                Errors.Add("Invalid email");
            }

            var newActivity = new Activity(signup.Activity.Id, signup.Activity.Name);
            if (!newActivity.ModelValid)
                Errors.Add("Invalid new activity--must be between 1 and 20 characters");
            else
                Activity = newActivity;

            // No validation on comments for now.
            Comments = signup.Comments;
        }


        // Implementation details -- not part of IActivitySignup
        public bool ModelValid => Errors is null || Errors.Count == 0;


        // Note for time saving, we're just hard coding our errors -- magic strings, but we could
        // have a more sophisticated error strategy that returns an error code and a user friendly
        // message perhaps from the databse or a resource file (for translations). Even if we don't 
        // use error codes, we can easily adopt this approach for having translations.
        public List<string> Errors { get; private set; }

        private void SetTimeOfDay(int minutes)
        {
            if (minutes < 0)
                Errors.Add("Time of day cannot be negative");
            else if (minutes > 60 * 24)
                Errors.Add("Time of day cannot be passed 12 PM");
            else
                TimeOfDayMinutes = minutes;
        }

        private void SetPrefferedStart(DateTime start)
        {
            if (start < DateTime.Today)
                Errors.Add("Start date cannot start before today");
            // Can add business logic on how long in advance start can be
            else
                PrefferedStart = start;
        }

        private void SetYearsExpereince(int years)
        {
            if (years < 0)
                Errors.Add("Years of experience cannot be negative");
            else if (years > 100)
                Errors.Add("You must be exageratting about your years of experience. Must be less than 100");
            else YearsExperience = years; 
        }

        // Let's use the same validation logic for first and last name
        private bool TryValidateName(string name, out string validatedName) {
            validatedName = "";
            if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
                return false;

            validatedName = name;
            return true;
        }

        // IActivitySignup Implementation -- normally I would put props higher up,
        // but when inheriting from an interface, I group the model interface impleemntation
        // and the implemenation details together
        public Guid Id { get; private set; } 
        public string FirstName { get; private set; } 
        public string LastName { get; private set; }
        public string Email => _email.Address; 
        public DateTime PrefferedStart { get; private set; } 
        public int TimeOfDayMinutes { get; private set; } 
        public int YearsExperience { get; private set; } 
        public IActivity Activity { get; private set; } 
        public string Comments { get; private set; } 
    }
}
