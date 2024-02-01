using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resources.Persistence.Repositories;
using Resources.Persistence.Repositories.Abstract;

namespace Resources.Persistence
{
    public static class ServiceCollectionSetup
    {
        public static IServiceCollection AddColdrunEntityContext(
            this IServiceCollection services,
            IConfiguration configuration)

        {
            var dbConnection = configuration.GetSection("ColdrunEntityDbConfiguration").Value;
            services.AddDbContext<ColdrunEntityDbContext>(
                optionsBuilder =>
                {
                    optionsBuilder.EnableSensitiveDataLogging();
                    optionsBuilder.EnableDetailedErrors();
                    optionsBuilder.UseSqlite(dbConnection);
                },
                ServiceLifetime.Transient);

            services.AddTransient<Func<ColdrunEntityDbContext>>(sp => sp.GetRequiredService<ColdrunEntityDbContext>);

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITruckRepository, TruckRepository>();

            return services;
        }
    }
}
