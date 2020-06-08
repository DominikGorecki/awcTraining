using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWC.TrainingEvents.Abstract.IModels;
using AWC.TrainingEvents.Abstract.IServices;
using AWC.TrainingEventsWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWC.TrainingEventsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService ?? throw new ArgumentNullException(nameof(activityService));
        }

        [HttpGet("activities")]
        public async Task<IActionResult> GetActivities()
        {
            var allActivitiesResponse = await _activityService.AllActivities();
            if (allActivitiesResponse.IsError) return BadRequest(allActivitiesResponse.ErrorSummary);
            var allActivitiesVM = allActivitiesResponse
                                    .Value
                                    .Select(a => new ActivityVM(a.Id, a.Name))
                                    .ToList();
            return Ok(allActivitiesVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignups()
        {
            // TODO--implement pagination or lazy loading if we have more than 50 signups
            var response = await _activityService.Signups();
            if (response.IsError) return BadRequest(response.ErrorSummary);
            var allSignupsVM = response
                                .Value
                                .Select(s => ActivitySignupVM.NewFrom(s))
                                .ToList();
            return Ok(allSignupsVM);
        }

        [HttpPut]
        public async Task<IActionResult> Put(NewSignupVM model)
        {
            var newSignupDTO = NewSignupDTO.NewFrom(model);
            var addResponse = await _activityService.NewSignup(newSignupDTO);
            if (addResponse.IsError) return BadRequest(addResponse.ErrorSummary);
            return Ok(ActivitySignupVM.NewFrom(addResponse.Value));
        }


    }
}