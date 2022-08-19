using Ergus.Backend.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ergus.Backend.Application.Setup
{
    public static class DependencyInjectionRepository
    {
        public static void AddDependencyInjectionApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IAdvertisementService, AdvertisementService>();
            services.AddSingleton<IAdvertisementSkuPriceService, AdvertisementSkuPriceService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ICurrencyService, CurrencyService>();
            services.AddSingleton<IGridService, GridService>();
            services.AddSingleton<IHorizontalVariationService, HorizontalVariationService>();
            services.AddSingleton<IPaymentFormService, PaymentFormService>();
            services.AddSingleton<IPriceListService, PriceListService>();
            services.AddSingleton<IProducerService, ProducerService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductAttributeService, ProductAttributeService>();
            services.AddSingleton<IProviderService, ProviderService>();
            services.AddSingleton<ISectionService, SectionService>();
            services.AddSingleton<ISkuService, SkuService>();
            services.AddSingleton<ISkuPriceService, SkuPriceService>();
            services.AddSingleton<IStockUnitService, StockUnitService>();
            services.AddSingleton<IUnitOfMeasureService, UnitOfMeasureService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IVerticalVariationService, VerticalVariationService>();
        }
    }
}
