using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Xunit;

namespace Ergus.Backend.Application.Tests.Helpers
{
    internal static class AssertHelper
    {
        public static void AssertEqual<T>(T expected, T actual) where T : IGeneric
        {
            Assert.Equal(expected.CreatedDate, actual.CreatedDate);
            Assert.Equal(expected.UpdatedDate, actual.UpdatedDate);
            Assert.Equal(expected.WasRemoved, actual.WasRemoved);
            Assert.Equal(expected.RemovedId, actual.RemovedId);
            Assert.Equal(expected.RemovedDate, actual.RemovedDate);
        }

        public static void AssertEqual(Advertisement expected, Advertisement actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.SkuCode, actual.SkuCode);
            Assert.Equal(expected.IntegrationCode, actual.IntegrationCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.AdvertisementType, actual.AdvertisementType);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal(expected.IntegrationId, actual.IntegrationId);
            Assert.Equal(expected.ProductId, actual.ProductId);

            AssertEqual<Advertisement>(expected, actual);
        }

        public static void AssertEqual(AdvertisementSkuPrice expected, AdvertisementSkuPrice actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.FictionalValue, actual.FictionalValue);
            Assert.Equal(expected.PromotionStart, actual.PromotionStart);
            Assert.Equal(expected.PromotionEnd, actual.PromotionEnd);
            Assert.Equal(expected.PriceListId, actual.PriceListId);
            Assert.Equal(expected.AdvertisementSkuId, actual.AdvertisementSkuId);

            AssertEqual<AdvertisementSkuPrice>(expected, actual);
        }

        public static void AssertEqual(Product expected, Product actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.SkuCode, actual.SkuCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.NCM, actual.NCM);
            Assert.Equal(expected.Active, actual.Active);
            Assert.Equal(expected.AdvertisementType, actual.AdvertisementType);
            Assert.Equal(expected.ProducerId, actual.ProducerId);
            Assert.Equal(expected.CategoryId, actual.CategoryId);
            Assert.Equal(expected.ProviderId, actual.ProviderId);

            AssertEqual<Product>(expected, actual);
        }
    }
}
