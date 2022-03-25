using Ergus.Backend.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Ergus.Backend.Infrastructure.Setup
{
    public static class DependencyInjectionCore
    {
        public static void AddDependencyInjectionCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITextEncryptor, AesTextEncryptor>();
        }
    }
}
