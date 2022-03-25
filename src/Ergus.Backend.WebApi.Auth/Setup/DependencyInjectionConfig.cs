using Ergus.Backend.Application.Setup;
using Ergus.Backend.Infrastructure.Setup;
using Ergus.Backend.WebApi.Auth.Services;

namespace Ergus.Backend.WebApi.Auth.Setup
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfigServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionCoreServices();

            services.AddDependencyInjectionRepositoryServices(configuration);

            services.AddDependencyInjectionApplicationServices();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
