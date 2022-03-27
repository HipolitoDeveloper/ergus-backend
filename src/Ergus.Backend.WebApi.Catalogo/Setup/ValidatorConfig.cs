using Ergus.Backend.Infrastructure.Repositories;
using Ergus.Backend.Infrastructure.Validations.Custom;

namespace Ergus.Backend.WebApi.Catalogo.Setup
{
    public static class ValidatorConfig
    {
        public static void UseValidatorConfigigure(this IApplicationBuilder app)
        {
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
        }
    }
}
