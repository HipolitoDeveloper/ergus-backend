using Ergus.Backend.Application.Setup;
using Ergus.Backend.Infrastructure.Setup;

namespace Ergus.Backend.WebApi.Catalogo.Setup
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfigServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionCoreServices();

            services.AddDependencyInjectionRepositoryServices(configuration);

            services.AddDependencyInjectionApplicationServices();
        }
    }
}
