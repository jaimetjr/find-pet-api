using Infrastructure.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddInfrastructureServices(configuration);
            return services;
        }

    }
}
