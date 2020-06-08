using AutoMapper;
using AWC.TrainingEvents.Abstract.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWC.TrainingEventsWeb.Models
{
    public class ActivitySignupVM
    {
        private static readonly IMapper _mapper;

        static ActivitySignupVM()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IActivitySignup, ActivitySignupVM>();
            });
            _mapper = config.CreateMapper();
        }

        public static ActivitySignupVM NewFrom(IActivitySignup model)
            => _mapper.Map<ActivitySignupVM>(model);

        public Guid Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public DateTime PrefferedStart { get; set; } 
        public int TimeOfDayMinutes { get; set; } 
        public int YearsExperience { get; set; } 
        public string Comments { get; set; } 
        public Guid ActivityId { get; set; }
        public string ActivityName { get; set; }
    }
}
