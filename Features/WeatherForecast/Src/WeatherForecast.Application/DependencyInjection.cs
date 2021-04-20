using System.Collections.Generic;
using System.Reflection;
using Draekien.CleanVerticalSlice.Common.Application;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            /* This configures the below from this application assembly
             * - AutoMapper profiles that implement IMapFrom
             * - Fluent Validators
             * - MediatR requests and handlers
             * - Pipeline behaviour for validating MediatR requests using Fluent Validation
             */
            services.AddCommonApplication(new List<Assembly> { typeof(DependencyInjection).Assembly });
        }
    }
}
