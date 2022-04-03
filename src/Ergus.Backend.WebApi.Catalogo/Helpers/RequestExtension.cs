using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Advertisements.Request;
using Ergus.Backend.WebApi.Catalogo.Models.AdvertisementSkuPrices.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Categories.Request;
using Ergus.Backend.WebApi.Catalogo.Models.PriceLists.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Products.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Providers.Request;
using Ergus.Backend.WebApi.Catalogo.Models.SkuPrices.Request;
using Ergus.Backend.WebApi.Catalogo.Models.Skus.Request;

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

        #endregion [ FIM - Category ]

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
    }
}
