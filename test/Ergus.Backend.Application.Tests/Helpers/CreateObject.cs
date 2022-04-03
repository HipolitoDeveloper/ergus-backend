using Ergus.Backend.Infrastructure.Models;
using System;

namespace Ergus.Backend.Application.Tests.Helpers
{
    internal static class CreateObject
    {
        public static Advertisement GetAdvertisement(int id, int? integrationId, int? productId)
        {
            return new Advertisement(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                skuCode: "SCOD",
                integrationCode: "ICOD",
                name: "Anuncio",
                advertisementType: Infrastructure.Helpers.TipoAnuncio.Gold,
                status: Infrastructure.Helpers.TipoStatusAnuncio.Ativo,
                integrationId: integrationId,
                productId: productId
            );
        }

        public static AdvertisementSku GetAdvertisementSku(int id)
        {
            return new AdvertisementSku(
                id: id,
                code: "COD"
            );
        }

        public static AdvertisementSkuPrice GetAdvertisementSkuPrice(int id, int? priceListId, int? advertisementSkuId)
        {
            return new AdvertisementSkuPrice(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                value: 1,
                fictionalValue: 2,
                promotionStart: DateTime.Now.AddDays(-1),
                promotionEnd: DateTime.Now.AddDays(1),
                priceListId: priceListId,
                advertisementSkuId: advertisementSkuId
            );
        }

        public static Category GetCategory(int id, int? parentId, CategoryText? text)
        {
            return new Category(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                name: "Categoria",
                active: true,
                parentId: parentId,
                text: text
            );
        }

        public static CategoryText GetCategoryText(int? id)
        {
            if (id == null)
                return CategoryText.Criar(
                    description: "DESC",
                    metaTitle: "TITLE",
                    metaKeyword: "KEYWORD",
                    metaDescription: "META DESC",
                    longDescription: null
                );

            return new CategoryText(
                id: id.Value,
                description: "DESC",
                metaTitle: "TITLE",
                metaKeyword: "KEYWORD",
                metaDescription: "META DESC",
                longDescription: null
            );
        }

        public static Integration GetIntegration(int id)
        {
            return new Integration(
                id: id,
                code: "COD"
            );
        }

        public static Metadata GetMetadata(int id)
        {
            return new Metadata(
                id: id
            );
        }

        public static PriceList GetPriceList(int id, int? parentId)
        {
            return new PriceList(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                initDate: DateTime.Now.AddDays(-1),
                endDate: DateTime.Now.AddDays(1),
                name: "Produto",
                value: 1,
                type: Infrastructure.Helpers.TipoListaPreco.Dinamico,
                adjustmentType: Infrastructure.Helpers.TipoAjuste.ValorFixo,
                operationType: Infrastructure.Helpers.TipoOperacao.Adicao,
                saleMaxAmount: 2,
                parentId: parentId
            );
        }

        public static Producer GetProducer(int id)
        {
            return new Producer(
                id: id,
                code: "COD"
            );
        }

        public static Product GetProduct(int id, int? producerId, int? categoryId, int? providerId)
        {
            return new Product(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                skuCode: "SCOD",
                name: "Produto",
                ncm: "NCM",
                advertisementType: Infrastructure.Helpers.TipoAnuncio.GoldSpecial,
                active: true,
                producerId: producerId,
                categoryId: categoryId,
                providerId: providerId
            );
        }

        public static ProductAttribute GetProductAttribute(int id, int? metadataId, int? productId)
        {
            return new ProductAttribute(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                metadataId: metadataId,
                productId: productId
            );
        }

        public static Address GetAddress(int? id)
        {
            return new Address(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                cityCode: "CITY",
                district: "Bairro",
                complement: "Complemento",
                number: "123",
                reference: "Referencia",
                zipCode: "12345-000",
                addressValue: "Rua Logradouro"
            );
        }

        public static Provider GetProvider(int id, Address? address)
        {
            return new Provider(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                name: "Fornecedor",
                email: "a@b.com",
                contact: "Contato",
                site: "Site",
                fiscalDocument: "Doc Fiscal",
                document: "Documento",
                personType: Infrastructure.Helpers.TipoPessoa.Fisica,
                active: true,
                address: address
            );
        }

        public static Sku GetSku(int id, int? productId)
        {
            return new Sku(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                skuCode: "SCOD",
                name: "Produto",
                reference: "Referencia",
                bar: "Bar",
                height: 1,
                width: 2,
                depth: 3,
                weight: 4,
                cost: 5,
                productId: productId
            );
        }

        public static SkuPrice GetSkuPrice(int id, int? priceListId, int? skuId)
        {
            return new SkuPrice(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                value: 1,
                fictionalValue: 2,
                promotionStart: DateTime.Now.AddDays(-1),
                promotionEnd: DateTime.Now.AddDays(1),
                priceListId: priceListId,
                skuId: skuId
            );
        }

        public static User GetUser(int id)
        {
            return new User(
                id: id,
                name: "Nome",
                login: "Login",
                password: "Password",
                email: "email@ab.com"
            );
        }
    }
}
