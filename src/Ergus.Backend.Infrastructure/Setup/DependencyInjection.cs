using Ergus.Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ergus.Backend.Infrastructure.Setup
{
    public static class DependencyInjectionRepository
    {
        public static void AddDependencyInjectionRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppClientContext>(o => { o.UseNpgsql(configuration.GetConnectionString("DefaultClient")); }, ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddDbContext<AppServerContext>(o => { o.UseNpgsql(configuration.GetConnectionString("DefaultServer")); }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            services.AddSingleton<IAddressRepository, AddressRepository>();
            services.AddSingleton<IAdvertisementRepository, AdvertisementRepository>();
            services.AddSingleton<IAdvertisementSkuRepository, AdvertisementSkuRepository>();
            services.AddSingleton<IAdvertisementSkuPriceRepository, AdvertisementSkuPriceRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IGridRepository, GridRepository>();
            services.AddSingleton<IHorizontalVariationRepository, HorizontalVariationRepository>();
            services.AddSingleton<IIntegrationRepository, IntegrationRepository>();
            services.AddSingleton<IMetadataRepository, MetadataRepository>();
            services.AddSingleton<IPriceListRepository, PriceListRepository>();
            services.AddSingleton<IProducerRepository, ProducerRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddSingleton<IProviderRepository, ProviderRepository>();
            services.AddSingleton<ISectionRepository, SectionRepository>();
            services.AddSingleton<ISkuRepository, SkuRepository>();
            services.AddSingleton<ISkuPriceRepository, SkuPriceRepository>();
            services.AddSingleton<IStockUnitRepository, StockUnitRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IVerticalVariationRepository, VerticalVariationRepository>();
        }
    }
}
