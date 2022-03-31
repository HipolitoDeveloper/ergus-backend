using Autofac.Extras.Moq;
using Ergus.Backend.Application.Tests.Helpers;
using Ergus.Backend.Infrastructure;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;
using Ergus.Backend.Infrastructure.Validations.Custom;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace Ergus.Backend.Application.Tests
{
    public class BaseTest : IDisposable
    {
        #region [ Propriedades ]

        private Mock<AppClientContext> _appClientContext;

        protected AutoMock _autoMock;

        protected Mock<IAdvertisementRepository> _mockAdvertisementRepository;
        protected Mock<IAdvertisementSkuRepository> _mockAdvertisementSkuRepository;
        protected Mock<IAdvertisementSkuPriceRepository> _mockAdvertisementSkuPriceRepository;
        protected Mock<ICategoryRepository> _mockCategoryRepository;
        protected Mock<IIntegrationRepository> _mockIntegrationRepository;
        protected Mock<IMetadataRepository> _mockMetadataRepository;
        protected Mock<IPriceListRepository> _mockPriceListRepository;
        protected Mock<IProducerRepository> _mockProducerRepository;
        protected Mock<IProductRepository> _mockProductRepository;
        protected Mock<IProductAttributeRepository> _mockProductAttributeRepository;
        protected Mock<IProviderRepository> _mockProviderRepository;
        protected Mock<ISkuRepository> _mockSkuRepository;
        protected Mock<ISkuPriceRepository> _mockSkuPriceRepository;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public BaseTest()
        {
            this._autoMock = AutoMock.GetLoose();
            ConfigureMockRepository();
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        #region [ Mocks ]

        public void MockGetAdvertisementSku(int advertisementSkuId)
        {
            var advertisementSku = CreateObject.GetAdvertisementSku(advertisementSkuId);
            this._mockAdvertisementSkuRepository.Setup(x => x.Get(advertisementSkuId, false)).ReturnsAsync(advertisementSku);
        }

        public void MockGetCategory(int categoryId)
        {
            var category = CreateObject.GetCategory(categoryId, null);
            this._mockCategoryRepository.Setup(x => x.Get(categoryId, false)).ReturnsAsync(category);
        }

        public void MockGetIntegration(int integrationId)
        {
            var integration = CreateObject.GetIntegration(integrationId);
            this._mockIntegrationRepository.Setup(x => x.Get(integrationId, false)).ReturnsAsync(integration);
        }

        public void MockGetPriceList(int priceListId)
        {
            var priceList = CreateObject.GetPriceList(priceListId, null);
            this._mockPriceListRepository.Setup(x => x.Get(priceListId, false)).ReturnsAsync(priceList);
        }

        public void MockGetProducer(int producerId)
        {
            var producer = CreateObject.GetProducer(producerId);
            this._mockProducerRepository.Setup(x => x.Get(producerId, false)).ReturnsAsync(producer);
        }

        public void MockGetProduct(int productId)
        {
            var product = CreateObject.GetProduct(productId, null, null, null);
            this._mockProductRepository.Setup(x => x.Get(productId, false)).ReturnsAsync(product);
        }

        public void MockGetProvider(int providerId)
        {
            var provider = CreateObject.GetProvider(providerId);
            this._mockProviderRepository.Setup(x => x.Get(providerId, false)).ReturnsAsync(provider);
        }

        #endregion [ FIM - Mocks ]

        private void ConfigureMockRepository()
        {
            _appClientContext = this._autoMock.Mock<AppClientContext>();

            #region [ Mock Repositories ]

            this._mockAdvertisementRepository = this._autoMock.Mock<IAdvertisementRepository>();
            this._mockAdvertisementSkuRepository = this._autoMock.Mock<IAdvertisementSkuRepository>();
            this._mockAdvertisementSkuPriceRepository = this._autoMock.Mock<IAdvertisementSkuPriceRepository>();
            this._mockCategoryRepository = this._autoMock.Mock<ICategoryRepository>();
            this._mockIntegrationRepository = this._autoMock.Mock<IIntegrationRepository>();
            this._mockMetadataRepository = this._autoMock.Mock<IMetadataRepository>();
            this._mockPriceListRepository = this._autoMock.Mock<IPriceListRepository>();
            this._mockProducerRepository = this._autoMock.Mock<IProducerRepository>();
            this._mockProductRepository = this._autoMock.Mock<IProductRepository>();
            this._mockProductAttributeRepository = this._autoMock.Mock<IProductAttributeRepository>();
            this._mockProviderRepository = this._autoMock.Mock<IProviderRepository>();
            this._mockSkuRepository = this._autoMock.Mock<ISkuRepository>();
            this._mockSkuPriceRepository = this._autoMock.Mock<ISkuPriceRepository>();

            #endregion [ FIM - Mock Repositories ]

            #region [ Mock Validators ]

            if (_mockAdvertisementRepository != null)
            {
                _appClientContext.Setup(x => x.Advertisements).Returns(new Mock<DbSet<Advertisement>>().Object);
                _mockAdvertisementRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockAdvertisementRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticAdvertisementExistsValidator.Configure(_mockAdvertisementRepository.Object);
                StaticAdvertisementCodeBeUniqueValidator.Configure(_mockAdvertisementRepository.Object);
            }

            if (_mockAdvertisementSkuRepository != null)
            {
                _appClientContext.Setup(x => x.AdvertisementSkus).Returns(new Mock<DbSet<AdvertisementSku>>().Object);
                _mockAdvertisementSkuRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockAdvertisementSkuRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticAdvertisementSkuExistsValidator.Configure(_mockAdvertisementSkuRepository.Object);
                StaticAdvertisementSkuCodeBeUniqueValidator.Configure(_mockAdvertisementSkuRepository.Object);
            }

            if (_mockAdvertisementSkuPriceRepository != null)
            {
                _appClientContext.Setup(x => x.AdvertisementSkuPrices).Returns(new Mock<DbSet<AdvertisementSkuPrice>>().Object);
                _mockAdvertisementSkuPriceRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockAdvertisementSkuPriceRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticAdvertisementSkuPriceExistsValidator.Configure(_mockAdvertisementSkuPriceRepository.Object);
                StaticAdvertisementSkuPriceCodeBeUniqueValidator.Configure(_mockAdvertisementSkuPriceRepository.Object);
            }

            if (_mockCategoryRepository != null)
            {
                _appClientContext.Setup(x => x.Categories).Returns(new Mock<DbSet<Category>>().Object);
                _mockCategoryRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockCategoryRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticCategoryExistsValidator.Configure(_mockCategoryRepository.Object);
                StaticCategoryCodeBeUniqueValidator.Configure(_mockCategoryRepository.Object);
            }

            if (_mockIntegrationRepository != null)
            {
                _appClientContext.Setup(x => x.Integrations).Returns(new Mock<DbSet<Integration>>().Object);
                _mockIntegrationRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockIntegrationRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticIntegrationExistsValidator.Configure(_mockIntegrationRepository.Object);
                StaticIntegrationCodeBeUniqueValidator.Configure(_mockIntegrationRepository.Object);
            }

            if (_mockMetadataRepository != null)
            {
                _appClientContext.Setup(x => x.Metadatas).Returns(new Mock<DbSet<Metadata>>().Object);
                _mockMetadataRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockMetadataRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticMetadataExistsValidator.Configure(_mockMetadataRepository.Object);
            }

            if (_mockPriceListRepository != null)
            {
                _appClientContext.Setup(x => x.PriceLists).Returns(new Mock<DbSet<PriceList>>().Object);
                _mockPriceListRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockPriceListRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticPriceListExistsValidator.Configure(_mockPriceListRepository.Object);
                StaticPriceListCodeBeUniqueValidator.Configure(_mockPriceListRepository.Object);
            }

            if (_mockProducerRepository != null)
            {
                _appClientContext.Setup(x => x.Producers).Returns(new Mock<DbSet<Producer>>().Object);
                _mockProducerRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockProducerRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticProducerExistsValidator.Configure(_mockProducerRepository.Object);
                StaticProducerCodeBeUniqueValidator.Configure(_mockProducerRepository.Object);
            }

            if (_mockProductRepository != null)
            {
                _appClientContext.Setup(x => x.Products).Returns(new Mock<DbSet<Product>>().Object);
                _mockProductRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockProductRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticProductExistsValidator.Configure(_mockProductRepository.Object);
                StaticProductCodeBeUniqueValidator.Configure(_mockProductRepository.Object);
            }

            if (_mockProductAttributeRepository != null)
            {
                _appClientContext.Setup(x => x.ProductAttributes).Returns(new Mock<DbSet<ProductAttribute>>().Object);
                _mockProductAttributeRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockProductAttributeRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticProductAttributeCodeBeUniqueValidator.Configure(_mockProductAttributeRepository.Object);
            }

            if (_mockProviderRepository != null)
            {
                _appClientContext.Setup(x => x.Providers).Returns(new Mock<DbSet<Provider>>().Object);
                _mockProviderRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockProviderRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticProviderExistsValidator.Configure(_mockProviderRepository.Object);
                StaticProviderCodeBeUniqueValidator.Configure(_mockProviderRepository.Object);
            }

            if (_mockSkuRepository != null)
            {
                _appClientContext.Setup(x => x.Skus).Returns(new Mock<DbSet<Sku>>().Object);
                _mockSkuRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockSkuRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticSkuExistsValidator.Configure(_mockSkuRepository.Object);
                StaticSkuCodeBeUniqueValidator.Configure(_mockSkuRepository.Object);
            }

            if (_mockSkuPriceRepository != null)
            {
                _appClientContext.Setup(x => x.SkuPrices).Returns(new Mock<DbSet<SkuPrice>>().Object);
                _mockSkuPriceRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockSkuPriceRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticSkuPriceExistsValidator.Configure(_mockSkuPriceRepository.Object);
                StaticSkuPriceCodeBeUniqueValidator.Configure(_mockSkuPriceRepository.Object);
            }

            #endregion [ FIM - Mock Validators ]
        }

        public void Dispose()
        {
            this._autoMock.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
