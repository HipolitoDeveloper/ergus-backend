using Ergus.Backend.Infrastructure.Repositories;
using Ergus.Backend.Infrastructure.Validations.Custom;

namespace Ergus.Backend.WebApi.Catalogo.Setup
{
    public static class ValidatorConfig
    {
        public static void UseValidatorConfigigure(this IApplicationBuilder app)
        {
            var addressRepository = app.ApplicationServices.GetService<IAddressRepository>();
            if (addressRepository != null)
            {
                StaticAddressExistsValidator.Configure(addressRepository);
                StaticAddressCodeBeUniqueValidator.Configure(addressRepository);
            }

            var advertisementRepository = app.ApplicationServices.GetService<IAdvertisementRepository>();
            if (advertisementRepository != null)
            {
                StaticAdvertisementExistsValidator.Configure(advertisementRepository);
                StaticAdvertisementCodeBeUniqueValidator.Configure(advertisementRepository);
            }

            var advertisementSkuRepository = app.ApplicationServices.GetService<IAdvertisementSkuRepository>();
            if (advertisementSkuRepository != null)
            {
                StaticAdvertisementSkuExistsValidator.Configure(advertisementSkuRepository);
                StaticAdvertisementSkuCodeBeUniqueValidator.Configure(advertisementSkuRepository);
            }

            var advertisementSkuPriceRepository = app.ApplicationServices.GetService<IAdvertisementSkuPriceRepository>();
            if (advertisementSkuPriceRepository != null)
            {
                StaticAdvertisementSkuPriceExistsValidator.Configure(advertisementSkuPriceRepository);
                StaticAdvertisementSkuPriceCodeBeUniqueValidator.Configure(advertisementSkuPriceRepository);
            }

            var categoryRepository = app.ApplicationServices.GetService<ICategoryRepository>();
            if (categoryRepository != null)
            {
                StaticCategoryExistsValidator.Configure(categoryRepository);
                StaticCategoryCodeBeUniqueValidator.Configure(categoryRepository);
            }

            var companyRepository = app.ApplicationServices.GetService<ICompanyRepository>();
            if (companyRepository != null)
            {
                StaticCompanyExistsValidator.Configure(companyRepository);
                StaticCompanyCodeBeUniqueValidator.Configure(companyRepository);
            }

            var gridRepository = app.ApplicationServices.GetService<IGridRepository>();
            if (gridRepository != null)
            {
                StaticGridExistsValidator.Configure(gridRepository);
                StaticGridCodeBeUniqueValidator.Configure(gridRepository);
            }

            var horizontalVariationRepository = app.ApplicationServices.GetService<IHorizontalVariationRepository>();
            if (horizontalVariationRepository != null)
            {
                StaticHorizontalVariationExistsValidator.Configure(horizontalVariationRepository);
                StaticHorizontalVariationCodeBeUniqueValidator.Configure(horizontalVariationRepository);
            }

            var integrationRepository = app.ApplicationServices.GetService<IIntegrationRepository>();
            if (integrationRepository != null)
            {
                StaticIntegrationExistsValidator.Configure(integrationRepository);
                StaticIntegrationCodeBeUniqueValidator.Configure(integrationRepository);
            }

            var metadataRepository = app.ApplicationServices.GetService<IMetadataRepository>();
            if (metadataRepository != null)
                StaticMetadataExistsValidator.Configure(metadataRepository);

            var priceListRepository = app.ApplicationServices.GetService<IPriceListRepository>();
            if (priceListRepository != null)
            {
                StaticPriceListExistsValidator.Configure(priceListRepository);
                StaticPriceListCodeBeUniqueValidator.Configure(priceListRepository);
            }

            var producerRepository = app.ApplicationServices.GetService<IProducerRepository>();
            if (producerRepository != null)
            {
                StaticProducerExistsValidator.Configure(producerRepository);
                StaticProducerCodeBeUniqueValidator.Configure(producerRepository);
            }

            var productRepository = app.ApplicationServices.GetService<IProductRepository>();
            if (productRepository != null)
            {
                StaticProductExistsValidator.Configure(productRepository);
                StaticProductCodeBeUniqueValidator.Configure(productRepository);
            }

            var productAttributeRepository = app.ApplicationServices.GetService<IProductAttributeRepository>();
            if (productAttributeRepository != null)
                StaticProductAttributeCodeBeUniqueValidator.Configure(productAttributeRepository);

            var providerRepository = app.ApplicationServices.GetService<IProviderRepository>();
            if (providerRepository != null)
            {
                StaticProviderExistsValidator.Configure(providerRepository);
                StaticProviderCodeBeUniqueValidator.Configure(providerRepository);
            }

            var sectionRepository = app.ApplicationServices.GetService<ISectionRepository>();
            if (sectionRepository != null)
            {
                StaticSectionExistsValidator.Configure(sectionRepository);
                StaticSectionCodeBeUniqueValidator.Configure(sectionRepository);
            }

            var skuRepository = app.ApplicationServices.GetService<ISkuRepository>();
            if (skuRepository != null)
            {
                StaticSkuExistsValidator.Configure(skuRepository);
                StaticSkuCodeBeUniqueValidator.Configure(skuRepository);
            }

            var skuPriceRepository = app.ApplicationServices.GetService<ISkuPriceRepository>();
            if (skuPriceRepository != null)
            {
                StaticSkuPriceExistsValidator.Configure(skuPriceRepository);
                StaticSkuPriceCodeBeUniqueValidator.Configure(skuPriceRepository);
            }

            var stockUnitRepository = app.ApplicationServices.GetService<IStockUnitRepository>();
            if (stockUnitRepository != null)
            {
                StaticStockUnitExistsValidator.Configure(stockUnitRepository);
                StaticStockUnitCodeBeUniqueValidator.Configure(stockUnitRepository);
            }

            var unitOfMeasureRepository = app.ApplicationServices.GetService<IUnitOfMeasureRepository>();
            if (unitOfMeasureRepository != null)
            {
                StaticUnitOfMeasureExistsValidator.Configure(unitOfMeasureRepository);
                StaticUnitOfMeasureCodeBeUniqueValidator.Configure(unitOfMeasureRepository);
            }

            var verticalVariationRepository = app.ApplicationServices.GetService<IVerticalVariationRepository>();
            if (verticalVariationRepository != null)
            {
                StaticVerticalVariationExistsValidator.Configure(verticalVariationRepository);
                StaticVerticalVariationCodeBeUniqueValidator.Configure(verticalVariationRepository);
            }
        }
    }
}
