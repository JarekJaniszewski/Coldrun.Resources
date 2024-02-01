using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Resources.Application.TruckManagement.Commands.CreateTruck;

namespace Resources.Application
{
    public static class ServiceCollectionSetup
    {
        public static IServiceCollection AddCustomMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateTruckCommandHandler).Assembly);
            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(
                new[]
                {
                    typeof(CreateTruckCommandValidator).Assembly
                });
            return services;
        }
    }
}
