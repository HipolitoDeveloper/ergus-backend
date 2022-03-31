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

        public static Category GetCategory(int id, int? parentId)
        {
            return new Category(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                name: "Categoria",
                active: true,
                parentId: parentId
            );
        }

        public static Integration GetIntegration(int id)
        {
            return new Integration(
                id: id,
                code: "COD"
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

        public static Provider GetProvider(int id)
        {
            return new Provider(
                id: id,
                code: "COD"
            );
        }
    }
}
