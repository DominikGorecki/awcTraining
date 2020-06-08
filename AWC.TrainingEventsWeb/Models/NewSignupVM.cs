using AutoMapper;
using AWC.TrainingEvents.Abstract.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWC.TrainingEventsWeb.Models
{

    public class NewSignupVM
    {
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

    public class NewSignupDTO : NewSignupVM, IActivitySignup
    {
        private static readonly IMapper _mapper;

        static NewSignupDTO()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewSignupVM, NewSignupDTO>();
            });
            _mapper = config.CreateMapper();
        }

        public static NewSignupDTO NewFrom(NewSignupVM model)
            => _mapper.Map<NewSignupDTO>(model);
        
        public Guid Id { get; set; }
        public IActivity Activity => new ActivityDTO(this);

        public class ActivityDTO : IActivity
        {
            public ActivityDTO(NewSignupDTO model)
            {
                Id = model.ActivityId;
                Name = model.ActivityName;
            }
            public Guid Id { get; private set; } 
            public string Name { get; private set; } 
        }
    }

}
