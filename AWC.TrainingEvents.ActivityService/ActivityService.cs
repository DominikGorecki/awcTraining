using AWC.TrainingEvents.Abstract.IModels;
using AWC.TrainingEvents.Abstract.IRepositories;
using AWC.TrainingEvents.Abstract.IServices;
using AWC.TrainingEvents.ActivityService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWC.TrainingEvents.ActivityService
{
    public class ActivityService : IActivityService
    {
        private IActivityData _activityData;

        public ActivityService(IActivityData activityData)
        {
            _activityData = activityData ?? throw new ArgumentNullException(nameof(activityData));
        }

        public Task<IResponse<IEnumerable<IActivitySignup>>> Signups(int skip = 0, int top = 0)
            => _activityData.GetAllSignups(skip, top);
        

        public async Task<IResponse<IActivitySignup>> NewSignup(IActivitySignup signup)
        {
            var newSignup = new ActivitySignup(signup);

            // Some business logic issue
            if (!newSignup.ModelValid) 
                return new Response<IActivitySignup>(newSignup.Errors);

            // Save new signup and return response right from the data layer since we don't nee any 
            // other business logic if save fails; however, we wanted the ActivityService to coordinate
            // some actions on fail, we would do that here. 
            return await _activityData.UpsertSignup(newSignup);
        }

        public Task<IResponse<IEnumerable<IActivity>>> AllActivities()
            => _activityData.AllActivities();
    }
}
