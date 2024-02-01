using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Resources.Api
{
    public static class ServiceCollectionSetup
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                    .AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }
    }
}
