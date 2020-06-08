using AWC.TrainingEvents.Abstract.IRepositories;
using AWC.TrainingEvents.Abstract.IServices;
using AWC.TrainingEvents.ActivityService;
using AWC.TrainingEvents.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWC.TrainingEventsWeb.Configuration
{
    public static class ConfigureActivityServices
    {
        // Using Microsoft's extension pattern for organizing the configuration of DI
        public static IServiceCollection AddActivityServices(this IServiceCollection services)
        {
            services.AddTransient<IActivityData, ActivityData>();
            services.AddTransient<IActivityService, ActivityService>();
            return services;
        }
    }
}
