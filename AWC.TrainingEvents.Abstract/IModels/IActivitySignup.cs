using System;
using System.Collections.Generic;
using System.Text;

namespace AWC.TrainingEvents.Abstract.IModels
{
    public interface IActivitySignup
    {
        Guid Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        DateTime PrefferedStart { get; }

        // To keep things simple for sorting, matching, etc, let's just keep the time of day 
        // as an integer of minutes from the start of day 12:00. 
        // IE. 17:00 (5:00 PM) would be kept as 1020
        int TimeOfDayMinutes { get; }

        int YearsExperience { get; }

        // Keeping it in a seperate model so that we can keep activities in a seperate table
        // opens up the possibility of a many-to-many relationship in the future as well as
        // grouping people by acitity. We can still make it seemless on the FE. 
        IActivity Activity { get; } 

        string Comments { get; }
    }
}
