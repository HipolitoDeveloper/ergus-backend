using Ergus.Backend.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ergus.Backend.Application.Setup
{
    public static class DependencyInjectionRepository
    {
        public static void AddDependencyInjectionApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IAdvertisementService, AdvertisementService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductAttributeService, ProductAttributeService>();
            services.AddSingleton<ISkuService, SkuService>();
            services.AddSingleton<IUserService, UserService>();
        }
    }
}
