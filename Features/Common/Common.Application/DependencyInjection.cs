using System.Reflection;

using Common.Application.Behaviours;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Common.Application
{
    public static class DependencyInjection
    {
        public static void AddCommonApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
