using Microsoft.Extensions.DependencyInjection;
using TWPL.Core.Infrastructure.Interfaces.IRepositories;
using TWPL.Core.Infrastructure.Repositories;

namespace TWPL.Core.Configurations
{
    public static class RepositoryServiceConfiguration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IFleetRepository, FleetRepository>();
        }
    }
}
