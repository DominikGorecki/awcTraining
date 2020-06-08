using AWC.TrainingEvents.Abstract.IModels;
using AWC.TrainingEvents.Abstract.IRepositories;
using AWC.TrainingEvents.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWC.TrainingEvents.Data
{
    public class ActivityData : IActivityData
    {
        private readonly ApplicationDbContext _context;

        public ActivityData(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IResponse<IEnumerable<IActivitySignup>>> GetAllSignups(int skip = 0, int top = 0)
        {
            // TODO - implement skip/top for pagintation
            var allSignups = await _context.Signups
                .Include(s => s.ActivityRMO)
                .ToListAsync();

            return new Response<IEnumerable<IActivitySignup>>(allSignups); 
                //allSignups.Select(s => (IActivitySignup)s));
        }

        public async Task<IResponse<IActivitySignup>> UpsertSignup(IActivitySignup signup)
        {
            // Note--not a full upsert. For now, it's just used to add a new signup.

            var newSignup = ActivitySignupRMO.NewFrom(signup);
            _context.Signups.Add(newSignup);
            await _context.SaveChangesAsync();

            if (newSignup.Id == Guid.Empty)
                return new Response<IActivitySignup>("Issue with adding signup to the DB");

            return new Response<IActivitySignup>(newSignup);
        }

        public async Task<IResponse<IEnumerable<IActivity>>> AllActivities()
        {
            var allActivities = await _context.Activities.ToListAsync();

            return new Response<IEnumerable<IActivity>>(allActivities);
        }

    }
}
