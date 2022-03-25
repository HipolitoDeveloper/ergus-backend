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
        }
    }
}
