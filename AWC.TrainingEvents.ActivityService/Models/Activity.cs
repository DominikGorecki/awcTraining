using AWC.TrainingEvents.Abstract.IModels;
using AWC.TrainingEvents.Abstract.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWC.TrainingEvents.ActivityService.Models
{
    // Let's keep the business logic internal--we can run unit tests on the Service
    internal class Activity : IActivity
    {
        public Activity(Guid id, string name)
        {
            ModelValid = true;
            // New 
            if (id == Guid.Empty)
            {
                if (string.IsNullOrWhiteSpace(name) || name.Length > 20)
                    ModelValid = false;
                else
                    Name = name;
            }
            else
            {
                // No need for validation--existing activity
                Id = id;
                Name = name;
            }
        }

        public bool ModelValid { get; private set; }

        public Guid Id { get; private set; } 
        public string Name { get; private set; } 
    }
}
