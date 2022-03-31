using Ergus.Backend.Infrastructure.Models;
using Xunit;

namespace Ergus.Backend.Application.Tests.Helpers
{
    internal static class AssertHelper
    {
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
        }
    }
}
