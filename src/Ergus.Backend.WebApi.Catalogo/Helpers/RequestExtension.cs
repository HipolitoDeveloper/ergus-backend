using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Advertisements.Request;
using Ergus.Backend.WebApi.Catalogo.Models.AdvertisementSkuPrices.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Categories.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Currencies.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Grids.Request;
using Ergus.Backend.WebApi.Catalogo.Models.HorizontalVariations.Request;
using Ergus.Backend.WebApi.Catalogo.Models.PaymentForms.Request;
using Ergus.Backend.WebApi.Catalogo.Models.PriceLists.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Producers.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Products.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Providers.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Sections.Request;
using Ergus.Backend.WebApi.Catalogo.Models.SkuPrices.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Skus.Request;
using Ergus.Backend.WebApi.Catalogo.Models.StockUnits.Request;
using Ergus.Backend.WebApi.Catalogo.Models.UnitOfMeasures.Request;
using Ergus.Backend.WebApi.Catalogo.Models.VerticalVariations.Request;

namespace Ergus.Backend.WebApi.Catalogo.Helpers
{
    public static class RequestExtension
    {
        #region [ Advertisement ]

        public static Advertisement? ToAdvertisement(this AdvertisementAddRequest request)
        {
            if (request == null)
                return null;

            var advertisement = Advertisement.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    skuCode: request.SkuCode!,
                    integrationCode: request.IntegrationCode!,
                    name: request.Name!,
                    advertisementType: request.AdvertisementType?.GetEnumValueFromDescription<TipoAnuncio>() ?? TipoAnuncio.None,
                    status: request.Status?.GetEnumValueFromDescription<TipoStatusAnuncio>() ?? TipoStatusAnuncio.Inativo,
                    integrationId: request.IntegrationId,
                    productId: request.ProductId
                );

            return advertisement;
        }

        public static Advertisement? ToAdvertisement(this AdvertisementUpdateRequest request)
        {
            if (request == null)
                return null;

            var advertisement = new Advertisement(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    skuCode: request.SkuCode!,
                    integrationCode: request.IntegrationCode!,
                    name: request.Name!,
                    advertisementType: request.AdvertisementType?.GetEnumValueFromDescription<TipoAnuncio>() ?? TipoAnuncio.None,
                    status: request.Status?.GetEnumValueFromDescription<TipoStatusAnuncio>() ?? TipoStatusAnuncio.Inativo,
                    integrationId: request.IntegrationId,
                    productId: request.ProductId
                );

            return advertisement;
        }

        #endregion [ FIM - Advertisement ]

        #region [ AdvertisementSkuPrice ]

        public static AdvertisementSkuPrice? ToAdvertisementSkuPrice(this AdvertisementSkuPriceAddRequest request)
        {
            if (request == null)
                return null;

            var advertisementSkuPrice = AdvertisementSkuPrice.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    value: request.Value!,
                    fictionalValue: request.FictionalValue!,
                    promotionStart: request.PromotionStart,
                    promotionEnd: request.PromotionEnd,
                    priceListId: request.PriceListId,
                    advertisementSkuId: request.AdvertisementSkuId
                );

            return advertisementSkuPrice;
        }

        public static AdvertisementSkuPrice? ToAdvertisementSkuPrice(this AdvertisementSkuPriceUpdateRequest request)
        {
            if (request == null)
                return null;

            var advertisementSkuPrice = new AdvertisementSkuPrice(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    value: request.Value!,
                    fictionalValue: request.FictionalValue!,
                    promotionStart: request.PromotionStart,
                    promotionEnd: request.PromotionEnd,
                    priceListId: request.PriceListId,
                    advertisementSkuId: request.AdvertisementSkuId
                );

            return advertisementSkuPrice;
        }

        #endregion [ FIM - AdvertisementSkuPrice ]

        #region [ Category ]

        public static Category? ToCategory(this CategoryAddRequest request)
        {
            if (request == null)
                return null;

            CategoryText? text = request.Text == null ? null : CategoryText.Criar(
                description: request.Text.Description,
                metaTitle: request.Text.MetaTitle,
                metaKeyword: request.Text.MetaKeyword,
                metaDescription: request.Text.MetaDescription,
                longDescription: request.Text.LongDescription
            );

            var category = Category.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    parentId: request.ParentId,
                    text: text
                );

            return category;
        }

        public static Category? ToCategory(this CategoryUpdateRequest request)
        {
            if (request == null)
                return null;

            CategoryText? text = request.Text == null ? null : new CategoryText(
                id: request.Id!,
                description: request.Text.Description,
                metaTitle: request.Text.MetaTitle,
                metaKeyword: request.Text.MetaKeyword,
                metaDescription: request.Text.MetaDescription,
                longDescription: request.Text.LongDescription
            );

            var category = new Category(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    active: request.Active,
                    parentId: request.ParentId,
                    text: text
                );

            return category;
        }

        #endregion [ FIM - Category ]

        #region [ Currency ]

        public static Currency? ToCurrency(this CurrencyAddRequest request)
        {
            if (request == null)
                return null;

            var currency = Currency.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    symbol: request.Symbol!
                );

            return currency;
        }

        public static Currency? ToCurrency(this CurrencyUpdateRequest request)
        {
            if (request == null)
                return null;

            var currency = new Currency(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    symbol: request.Symbol!
                );

            return currency;
        }

        #endregion [ FIM - Currency ]

        #region [ Grid ]

        public static Grid? ToGrid(this GridAddRequest request)
        {
            if (request == null)
                return null;

            var grid = Grid.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!
                );

            return grid;
        }

        public static Grid? ToGrid(this GridUpdateRequest request)
        {
            if (request == null)
                return null;

            var grid = new Grid(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!
                );

            return grid;
        }

        #endregion [ FIM - Grid ]

        #region [ HorizontalVariation ]

        public static HorizontalVariation? ToHorizontalVariation(this HorizontalVariationAddRequest request)
        {
            if (request == null)
                return null;

            var horizontalVariation = HorizontalVariation.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    strInterface: request.Interface!,
                    color: request.Color!,
                    order: request.Order ?? 0,
                    gridId: request.GridId
                );

            return horizontalVariation;
        }

        public static HorizontalVariation? ToHorizontalVariation(this HorizontalVariationUpdateRequest request)
        {
            if (request == null)
                return null;

            var horizontalVariation = new HorizontalVariation(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    strInterface: request.Interface!,
                    color: request.Color!,
                    order: request.Order ?? 0,
                    gridId: request.GridId
                );

            return horizontalVariation;
        }

        #endregion [ FIM - HorizontalVariation ]

        #region [ PaymentForm ]

        public static PaymentForm? ToPaymentForm(this PaymentFormAddRequest request)
        {
            if (request == null)
                return null;

            var paymentForm = PaymentForm.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    providerId: request.ProviderId
                );

            return paymentForm;
        }

        public static PaymentForm? ToPaymentForm(this PaymentFormUpdateRequest request)
        {
            if (request == null)
                return null;

            var paymentForm = new PaymentForm(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    active: request.Active,
                    providerId: request.ProviderId
                );

            return paymentForm;
        }

        #endregion [ FIM - PaymentForm ]

        #region [ PriceList ]

        public static PriceList? ToPriceList(this PriceListAddRequest request)
        {
            if (request == null)
                return null;

            var priceList = PriceList.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    initDate: request.InitDate!,
                    endDate: request.EndDate!,
                    name: request.Name!,
                    value: request.Value,
                    type: request.Type?.GetEnumValueFromDescription<TipoListaPreco>() ?? TipoListaPreco.Nenhum,
                    adjustmentType: request.AdjustmentType?.GetEnumValueFromDescription<TipoAjuste>() ?? TipoAjuste.Nenhum,
                    operationType: request.OperationType?.GetEnumValueFromDescription<TipoOperacao>() ?? TipoOperacao.Nenhum,
                    saleMaxAmount: request.SaleMaxAmount,
                    parentId: request.ParentId
                );

            return priceList;
        }

        public static PriceList? ToPriceList(this PriceListUpdateRequest request)
        {
            if (request == null)
                return null;

            var priceList = new PriceList(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    initDate: request.InitDate!,
                    endDate: request.EndDate!,
                    name: request.Name!,
                    value: request.Value,
                    type: request.Type?.GetEnumValueFromDescription<TipoListaPreco>() ?? TipoListaPreco.Nenhum,
                    adjustmentType: request.AdjustmentType?.GetEnumValueFromDescription<TipoAjuste>() ?? TipoAjuste.Nenhum,
                    operationType: request.OperationType?.GetEnumValueFromDescription<TipoOperacao>() ?? TipoOperacao.Nenhum,
                    saleMaxAmount: request.SaleMaxAmount,
                    parentId: request.ParentId
                );

            return priceList;
        }

        #endregion [ FIM - PriceList ]

        #region [ Producer ]

        public static Producer? ToProducer(this ProducerAddRequest request)
        {
            if (request == null)
                return null;

            Address? address = request.Address == null ? null : Address.Criar(
                code: request.Address?.Code!,
                externalCode: request.Address?.ExternalCode!,
                cityCode: request.Address?.CityCode!,
                district: request.Address?.District!,
                complement: request.Address?.Complement!,
                number: request.Address?.Number!,
                reference: request.Address?.Reference!,
                zipCode: request.Address?.ZipCode!,
                addressValue: request.Address?.AddressValue!
            );

            if (address == null && request.AddressId != null)
            {
                address = new Address();
                address.DefinirId(request.AddressId.Value);
            }

            var provider = Producer.Criar(
                code: request.Code!,
                externalCode: request.ExternalCode!,
                name: request.Name!,
                email: request.Email!,
                contact: request.Contact!,
                site: request.Site!,
                fiscalDocument: request.FiscalDocument!,
                document: request.Document!,
                personType: request.PersonType?.GetEnumValueFromDescription<TipoPessoa>() ?? TipoPessoa.Nenhum,
                address: address
            );

            return provider;
        }

        public static Producer? ToProducer(this ProducerUpdateRequest request)
        {
            if (request == null)
                return null;

            Address? address = request.Address == null ? null : new Address(
                id: request.Address?.Id!,
                code: request.Address?.Code!,
                externalCode: request.Address?.ExternalCode!,
                cityCode: request.Address?.CityCode!,
                district: request.Address?.District!,
                complement: request.Address?.Complement!,
                number: request.Address?.Number!,
                reference: request.Address?.Reference!,
                zipCode: request.Address?.ZipCode!,
                addressValue: request.Address?.AddressValue!
            );

            if (address == null && request.AddressId != null)
            {
                address = new Address();
                address.DefinirId(request.AddressId.Value);
            }

            var provider = new Producer(
                id: request.Id!,
                code: request.Code!,
                externalCode: request.ExternalCode!,
                name: request.Name!,
                email: request.Email!,
                contact: request.Contact!,
                site: request.Site!,
                fiscalDocument: request.FiscalDocument!,
                document: request.Document!,
                personType: request.PersonType?.GetEnumValueFromDescription<TipoPessoa>() ?? TipoPessoa.Nenhum,
                active: request.Active,
                address: address
            );

            return provider;
        }

        #endregion [ FIM - Producer ]

        #region [ Product ]

        public static Product? ToProduct(this ProductAddRequest request)
        {
            if (request == null)
                return null;

            var product = Product.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    skuCode: request.SkuCode!,
                    name: request.Name!,
                    ncm: request.NCM!,
                    advertisementType: request.AdvertisementType?.GetEnumValueFromDescription<TipoAnuncio>() ?? TipoAnuncio.None,
                    categoryId: request.CategoryId,
                    producerId: request.ProducerId,
                    providerId: request.ProviderId
                );

            return product;
        }

        public static Product? ToProduct(this ProductUpdateRequest request)
        {
            if (request == null)
                return null;

            var product = new Product(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    skuCode: request.SkuCode!,
                    name: request.Name!,
                    ncm: request.NCM!,
                    advertisementType: request.AdvertisementType?.GetEnumValueFromDescription<TipoAnuncio>() ?? TipoAnuncio.None,
                    active: request.Active,
                    categoryId: request.CategoryId,
                    producerId: request.ProducerId,
                    providerId: request.ProviderId
                );

            return product;
        }

        #endregion [ FIM - Product ]

        #region [ ProductAttribute ]

        public static ProductAttribute? ToProductAttribute(this ProductAttributeAddRequest request)
        {
            if (request == null)
                return null;

            var productAttribute = ProductAttribute.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    metadataId: request.MetadataId,
                    productId: request.ProductId
                );

            return productAttribute;
        }

        public static ProductAttribute? ToProductAttribute(this ProductAttributeUpdateRequest request)
        {
            if (request == null)
                return null;

            var productAttribute = new ProductAttribute(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    metadataId: request.MetadataId,
                    productId: request.ProductId
                );

            return productAttribute;
        }

        #endregion [ FIM - ProductAttribute ]

        #region [ Provider ]

        public static Provider? ToProvider(this ProviderAddRequest request)
        {
            if (request == null)
                return null;

            Address? address = request.Address == null ? null : Address.Criar(
                code: request.Address?.Code!,
                externalCode: request.Address?.ExternalCode!,
                cityCode: request.Address?.CityCode!,
                district: request.Address?.District!,
                complement: request.Address?.Complement!,
                number: request.Address?.Number!,
                reference: request.Address?.Reference!,
                zipCode: request.Address?.ZipCode!,
                addressValue: request.Address?.AddressValue!
            );

            if (address == null && request.AddressId != null)
            {
                address = new Address();
                address.DefinirId(request.AddressId.Value);
            }

            var provider = Provider.Criar(
                code: request.Code!,
                externalCode: request.ExternalCode!,
                name: request.Name!,
                email: request.Email!,
                contact: request.Contact!,
                site: request.Site!,
                fiscalDocument: request.FiscalDocument!,
                document: request.Document!,
                personType: request.PersonType?.GetEnumValueFromDescription<TipoPessoa>() ?? TipoPessoa.Nenhum,
                address: address
            );

            return provider;
        }

        public static Provider? ToProvider(this ProviderUpdateRequest request)
        {
            if (request == null)
                return null;

            Address? address = request.Address == null ? null : new Address (
                id: request.Address?.Id!,
                code: request.Address?.Code!,
                externalCode: request.Address?.ExternalCode!,
                cityCode: request.Address?.CityCode!,
                district: request.Address?.District!,
                complement: request.Address?.Complement!,
                number: request.Address?.Number!,
                reference: request.Address?.Reference!,
                zipCode: request.Address?.ZipCode!,
                addressValue: request.Address?.AddressValue!
            );

            if (address == null && request.AddressId != null)
            {
                address = new Address();
                address.DefinirId(request.AddressId.Value);
            }

            var provider = new Provider(
                id: request.Id!,
                code: request.Code!,
                externalCode: request.ExternalCode!,
                name: request.Name!,
                email: request.Email!,
                contact: request.Contact!,
                site: request.Site!,
                fiscalDocument: request.FiscalDocument!,
                document: request.Document!,
                personType: request.PersonType?.GetEnumValueFromDescription<TipoPessoa>() ?? TipoPessoa.Nenhum,
                active: request.Active,
                address: address
            );

            return provider;
        }

        #endregion [ FIM - Provider ]

        #region [ Section ]

        public static Section? ToSection(this SectionAddRequest request)
        {
            if (request == null)
                return null;

            var section = Section.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!
                );

            return section;
        }

        public static Section? ToSection(this SectionUpdateRequest request)
        {
            if (request == null)
                return null;

            var section = new Section(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!
                );

            return section;
        }

        #endregion [ FIM - Section ]

        #region [ Sku ]

        public static Sku? ToSku(this SkuAddRequest request)
        {
            if (request == null)
                return null;

            var sku = Sku.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    skuCode: request.SkuCode!,
                    name: request.Name!,
                    reference: request.Reference!,
                    bar: request.Bar!,
                    height: request.Height ?? 0,
                    width: request.Width ?? 0,
                    depth: request.Depth ?? 0,
                    weight: request.Weight ?? 0,
                    cost: request.Cost ?? 0,
                    productId: request.ProductId
                );

            return sku;
        }

        public static Sku? ToSku(this SkuUpdateRequest request)
        {
            if (request == null)
                return null;

            var sku = new Sku(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    skuCode: request.SkuCode!,
                    name: request.Name!,
                    reference: request.Reference!,
                    bar: request.Bar!,
                    height: request.Height ?? 0,
                    width: request.Width ?? 0,
                    depth: request.Depth ?? 0,
                    weight: request.Weight ?? 0,
                    cost: request.Cost ?? 0,
                    productId: request.ProductId
                );

            return sku;
        }

        #endregion [ FIM - Sku ]

        #region [ SkuPrice ]

        public static SkuPrice? ToSkuPrice(this SkuPriceAddRequest request)
        {
            if (request == null)
                return null;

            var skuPrice = SkuPrice.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    value: request.Value!,
                    fictionalValue: request.FictionalValue!,
                    promotionStart: request.PromotionStart,
                    promotionEnd: request.PromotionEnd,
                    priceListId: request.PriceListId,
                    skuId: request.SkuId
                );

            return skuPrice;
        }

        public static SkuPrice? ToSkuPrice(this SkuPriceUpdateRequest request)
        {
            if (request == null)
                return null;

            var skuPrice = new SkuPrice(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    value: request.Value!,
                    fictionalValue: request.FictionalValue!,
                    promotionStart: request.PromotionStart,
                    promotionEnd: request.PromotionEnd,
                    priceListId: request.PriceListId,
                    skuId: request.SkuId
                );

            return skuPrice;
        }

        #endregion [ FIM - AdvertisementSkuPrice ]

        #region [ StockUnit ]

        public static StockUnit? ToStockUnit(this StockUnitAddRequest request)
        {
            if (request == null)
                return null;

            var stockUnit = StockUnit.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    complement: request.Complement,
                    addressId: request.AddressId,
                    companyId: request.CompanyId
                );

            return stockUnit;
        }

        public static StockUnit? ToStockUnit(this StockUnitUpdateRequest request)
        {
            if (request == null)
                return null;

            var stockUnit = new StockUnit(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    complement: request.Complement,
                    addressId: request.AddressId,
                    companyId: request.CompanyId
                );

            return stockUnit;
        }

        #endregion [ FIM - StockUnit ]

        #region [ UnitOfMeasure ]

        public static UnitOfMeasure? ToUnitOfMeasure(this UnitOfMeasureAddRequest request)
        {
            if (request == null)
                return null;

            var unitOfMeasure = UnitOfMeasure.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    description: request.Description!,
                    acronym: request.Acronym
                );

            return unitOfMeasure;
        }

        public static UnitOfMeasure? ToUnitOfMeasure(this UnitOfMeasureUpdateRequest request)
        {
            if (request == null)
                return null;

            var unitOfMeasure = new UnitOfMeasure(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    description: request.Description!,
                    acronym: request.Acronym
                );

            return unitOfMeasure;
        }

        #endregion [ FIM - UnitOfMeasure ]

        #region [ VerticalVariation ]

        public static VerticalVariation? ToVerticalVariation(this VerticalVariationAddRequest request)
        {
            if (request == null)
                return null;

            var verticalVariation = VerticalVariation.Criar(
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    strInterface: request.Interface!,
                    order: request.Order ?? 0,
                    gridId: request.GridId
                );

            return verticalVariation;
        }

        public static VerticalVariation? ToVerticalVariation(this VerticalVariationUpdateRequest request)
        {
            if (request == null)
                return null;

            var verticalVariation = new VerticalVariation(
                    id: request.Id!,
                    code: request.Code!,
                    externalCode: request.ExternalCode!,
                    name: request.Name!,
                    strInterface: request.Interface!,
                    order: request.Order ?? 0,
                    gridId: request.GridId
                );

            return verticalVariation;
        }

        #endregion [ FIM - VerticalVariation ]
    }
}
