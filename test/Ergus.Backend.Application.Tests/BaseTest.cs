using Autofac.Extras.Moq;
using Ergus.Backend.Application.Tests.Helpers;
using Ergus.Backend.Infrastructure;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;
using Ergus.Backend.Infrastructure.Validations.Custom;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace Ergus.Backend.Application.Tests
{
    [Collection(nameof(SystemTestCollectionDefinition))]
    public class BaseTest : IDisposable
    {
        #region [ Propriedades ]

        private Mock<AppClientContext> _appClientContext;
        private Mock<AppServerContext> _appServerContext;

        protected AutoMock _autoMock;
        
        protected Mock<IAddressRepository> _mockAddressRepository;
        protected Mock<IAdvertisementRepository> _mockAdvertisementRepository;
        protected Mock<IAdvertisementSkuRepository> _mockAdvertisementSkuRepository;
        protected Mock<IAdvertisementSkuPriceRepository> _mockAdvertisementSkuPriceRepository;
        protected Mock<ICategoryRepository> _mockCategoryRepository;
        protected Mock<IGridRepository> _mockGridRepository;
        protected Mock<IHorizontalVariationRepository> _mockHorizontalVariationRepository;
        protected Mock<IIntegrationRepository> _mockIntegrationRepository;
        protected Mock<IMetadataRepository> _mockMetadataRepository;
        protected Mock<IPriceListRepository> _mockPriceListRepository;
        protected Mock<IProducerRepository> _mockProducerRepository;
        protected Mock<IProductRepository> _mockProductRepository;
        protected Mock<IProductAttributeRepository> _mockProductAttributeRepository;
        protected Mock<IProviderRepository> _mockProviderRepository;
        protected Mock<ISectionRepository> _mockSectionRepository;
        protected Mock<ISkuRepository> _mockSkuRepository;
        protected Mock<ISkuPriceRepository> _mockSkuPriceRepository;
        protected Mock<IStockUnitRepository> _mockStockUnitRepository;
        protected Mock<IUserRepository> _mockUserRepository;
        protected Mock<IVerticalVariationRepository> _mockVerticalVariationRepository;

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

        public void MockGetAddress(int addressId)
        {
            this._mockAddressRepository.Reset();

            var address = CreateObject.GetAddress(addressId);
            this._mockAddressRepository.Setup(x => x.Get(addressId, false)).ReturnsAsync(address);
        }

        public void MockGetAdvertisementSku(int advertisementSkuId)
        {
            this._mockAdvertisementSkuRepository.Reset();

            var advertisementSku = CreateObject.GetAdvertisementSku(advertisementSkuId);
            this._mockAdvertisementSkuRepository.Setup(x => x.Get(advertisementSkuId, false)).ReturnsAsync(advertisementSku);
        }

        public void MockGetCategory(int categoryId)
        {
            this._mockCategoryRepository.Reset();

            var category = CreateObject.GetCategory(categoryId, null, null);
            this._mockCategoryRepository.Setup(x => x.Get(categoryId, false)).ReturnsAsync(category);
        }

        public void MockGetGrid(int gridId)
        {
            this._mockGridRepository.Reset();

            var grid = CreateObject.GetGrid(gridId);
            this._mockGridRepository.Setup(x => x.Get(gridId, false)).ReturnsAsync(grid);
        }

        public void MockGetIntegration(int integrationId)
        {
            this._mockIntegrationRepository.Reset();

            var integration = CreateObject.GetIntegration(integrationId);
            this._mockIntegrationRepository.Setup(x => x.Get(integrationId, false)).ReturnsAsync(integration);
        }

        public void MockGetMetadata(int metadataId)
        {
            this._mockMetadataRepository.Reset();

            var metadata = CreateObject.GetMetadata(metadataId);
            this._mockMetadataRepository.Setup(x => x.Get(metadataId, false)).ReturnsAsync(metadata);
        }

        public void MockGetPriceList(int priceListId)
        {
            this._mockPriceListRepository.Reset();

            var priceList = CreateObject.GetPriceList(priceListId, null);
            this._mockPriceListRepository.Setup(x => x.Get(priceListId, false)).ReturnsAsync(priceList);
        }

        public void MockGetProducer(int producerId)
        {
            this._mockProducerRepository.Reset();

            var producer = CreateObject.GetProducer(producerId, null);
            this._mockProducerRepository.Setup(x => x.Get(producerId, false)).ReturnsAsync(producer);
        }

        public void MockGetProduct(int productId)
        {
            this._mockProductRepository.Reset();

            var product = CreateObject.GetProduct(productId, null, null, null);
            this._mockProductRepository.Setup(x => x.Get(productId, false)).ReturnsAsync(product);
        }

        public void MockGetProvider(int providerId)
        {
            this._mockProviderRepository.Reset();

            var provider = CreateObject.GetProvider(providerId, null);
            this._mockProviderRepository.Setup(x => x.Get(providerId, false)).ReturnsAsync(provider);
        }

        public void MockGetSection(int sectionId)
        {
            this._mockSectionRepository.Reset();

            var section = CreateObject.GetSection(sectionId);
            this._mockSectionRepository.Setup(x => x.Get(sectionId, false)).ReturnsAsync(section);
        }

        public void MockGetSku(int skuId)
        {
            this._mockSkuRepository.Reset();

            var sku = CreateObject.GetSku(skuId, null);
            this._mockSkuRepository.Setup(x => x.Get(skuId, false)).ReturnsAsync(sku);
        }

        public void MockGetUser(int userId)
        {
            this._mockUserRepository.Reset();

            var user = CreateObject.GetUser(userId);
            this._mockUserRepository.Setup(x => x.Get(userId, false)).ReturnsAsync(user);
        }

        #endregion [ FIM - Mocks ]

        private void ConfigureMockRepository()
        {
            _appClientContext = this._autoMock.Mock<AppClientContext>();
            _appServerContext = this._autoMock.Mock<AppServerContext>();

            #region [ Mock Repositories ]

            this._mockAddressRepository = this._autoMock.Mock<IAddressRepository>();
            this._mockAdvertisementRepository = this._autoMock.Mock<IAdvertisementRepository>();
            this._mockAdvertisementSkuRepository = this._autoMock.Mock<IAdvertisementSkuRepository>();
            this._mockAdvertisementSkuPriceRepository = this._autoMock.Mock<IAdvertisementSkuPriceRepository>();
            this._mockCategoryRepository = this._autoMock.Mock<ICategoryRepository>();
            this._mockGridRepository = this._autoMock.Mock<IGridRepository>();
            this._mockHorizontalVariationRepository = this._autoMock.Mock<IHorizontalVariationRepository>();
            this._mockIntegrationRepository = this._autoMock.Mock<IIntegrationRepository>();
            this._mockMetadataRepository = this._autoMock.Mock<IMetadataRepository>();
            this._mockPriceListRepository = this._autoMock.Mock<IPriceListRepository>();
            this._mockProducerRepository = this._autoMock.Mock<IProducerRepository>();
            this._mockProductRepository = this._autoMock.Mock<IProductRepository>();
            this._mockProductAttributeRepository = this._autoMock.Mock<IProductAttributeRepository>();
            this._mockProviderRepository = this._autoMock.Mock<IProviderRepository>();
            this._mockSectionRepository = this._autoMock.Mock<ISectionRepository>();
            this._mockSkuRepository = this._autoMock.Mock<ISkuRepository>();
            this._mockSkuPriceRepository = this._autoMock.Mock<ISkuPriceRepository>();
            this._mockStockUnitRepository = this._autoMock.Mock<IStockUnitRepository>();
            this._mockUserRepository = this._autoMock.Mock<IUserRepository>();
            this._mockVerticalVariationRepository = this._autoMock.Mock<IVerticalVariationRepository>();

            #endregion [ FIM - Mock Repositories ]

            #region [ Mock DbContext e Validators ]

            if (_mockAddressRepository != null)
            {
                _appClientContext.Setup(x => x.Addresses).Returns(new Mock<DbSet<Address>>().Object);
                _mockAddressRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockAddressRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticAddressExistsValidator.Configure(_mockAddressRepository.Object);
                StaticAddressCodeBeUniqueValidator.Configure(_mockAddressRepository.Object);
            }

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

            if (_mockGridRepository != null)
            {
                _appClientContext.Setup(x => x.Grids).Returns(new Mock<DbSet<Grid>>().Object);
                _mockGridRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockGridRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticGridExistsValidator.Configure(_mockGridRepository.Object);
                StaticGridCodeBeUniqueValidator.Configure(_mockGridRepository.Object);
            }

            if (_mockHorizontalVariationRepository != null)
            {
                _appClientContext.Setup(x => x.HorizontalVariations).Returns(new Mock<DbSet<HorizontalVariation>>().Object);
                _mockHorizontalVariationRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockHorizontalVariationRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticHorizontalVariationExistsValidator.Configure(_mockHorizontalVariationRepository.Object);
                StaticHorizontalVariationCodeBeUniqueValidator.Configure(_mockHorizontalVariationRepository.Object);
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

            if (_mockSectionRepository != null)
            {
                _appClientContext.Setup(x => x.Sections).Returns(new Mock<DbSet<Section>>().Object);
                _mockSectionRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockSectionRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticSectionExistsValidator.Configure(_mockSectionRepository.Object);
                StaticSectionCodeBeUniqueValidator.Configure(_mockSectionRepository.Object);
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

            if (_mockStockUnitRepository != null)
            {
                _appClientContext.Setup(x => x.StockUnits).Returns(new Mock<DbSet<StockUnit>>().Object);
                _mockStockUnitRepository.Setup(x => x.UnitOfWork).Returns(_appClientContext.Object);
                _mockStockUnitRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticStockUnitExistsValidator.Configure(_mockStockUnitRepository.Object);
                StaticStockUnitCodeBeUniqueValidator.Configure(_mockStockUnitRepository.Object);
            }

            if (_mockUserRepository != null)
            {
                _appServerContext.Setup(x => x.Users).Returns(new Mock<DbSet<User>>().Object);
                _mockUserRepository.Setup(x => x.UnitOfWork).Returns(_appServerContext.Object);
                _mockUserRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
            }

            if (_mockVerticalVariationRepository != null)
            {
                _appClientContext.Setup(x => x.VerticalVariations).Returns(new Mock<DbSet<VerticalVariation>>().Object);
                _mockVerticalVariationRepository.Setup(x => x.UnitOfWork).Returns(_appServerContext.Object);
                _mockVerticalVariationRepository.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);
                StaticVerticalVariationExistsValidator.Configure(_mockVerticalVariationRepository.Object);
                StaticVerticalVariationCodeBeUniqueValidator.Configure(_mockVerticalVariationRepository.Object);
            }

            #endregion [ FIM - Mock DbContext e Validators ]
        }

        public void Dispose()
        {
            this._autoMock.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
