using AWC.TrainingEvents.Abstract.IModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWC.TrainingEvents.Abstract.IServices
{
    public interface IActivityService
    {
        Task<IResponse<IActivitySignup>> NewSignup(IActivitySignup signup);
        Task<IResponse<IEnumerable<IActivitySignup>>> Signups(int skip = 0, int top = 0);

        // will use this for building a nice activity selector
        Task<IResponse<IEnumerable<IActivity>>> AllActivities();
    }
}
