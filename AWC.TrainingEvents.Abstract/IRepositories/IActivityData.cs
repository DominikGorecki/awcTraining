using AWC.TrainingEvents.Abstract.IModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWC.TrainingEvents.Abstract.IRepositories
{
    public interface IActivityData
    {
        /// <summary>
        /// Right now only used for new signups--but if we add auth can be used for modifying
        /// </summary>
        /// <param name="signup"></param>
        /// <returns></returns>
        Task<IResponse<IActivitySignup>> UpsertSignup(IActivitySignup signup);

        /// <summary>
        /// To save time, I'm not going to implement pagination (or lazy loading) in the UI 
        /// but the call should support it from the outset.
        /// </summary>
        /// <param name="skip">entries to skip</param>
        /// <param name="top">how many to get</param>
        /// <returns></returns>
        Task<IResponse<IEnumerable<IActivitySignup>>> GetAllSignups(int skip = 0, int top = 0);

        /// <summary>
        /// Get specific entry-- can be used in the future when we want to have auth a way to 
        /// modify your signup.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<IResponse<IEnumerable<IActivitySignup>>> GetSignup(Guid id);

        Task<IResponse<IEnumerable<IActivity>>> AllActivities();
    }
}
