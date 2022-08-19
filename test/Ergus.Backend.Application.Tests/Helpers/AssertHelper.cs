using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using System;
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

        public static void AssertAddUpdate<T>(T? actual) where T : BaseModel
        {
            Assert.NotNull(actual);
            Assert.True(actual!.IsValid, String.Join(',', actual.Erros));
            Assert.Empty(actual.Erros);
        }

        public static void AssertEqual(Address expected, Address actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.CityCode, actual.CityCode);
            Assert.Equal(expected.District, actual.District);
            Assert.Equal(expected.Complement, actual.Complement);
            Assert.Equal(expected.Number, actual.Number);
            Assert.Equal(expected.Reference, actual.Reference);
            Assert.Equal(expected.ZipCode, actual.ZipCode);
            Assert.Equal(expected.AddressValue, actual.AddressValue);

            AssertEqual<Address>(expected, actual);
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

        public static void AssertEqual(Category expected, Category actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Active, actual.Active);
            Assert.Equal(expected.ParentId, actual.ParentId);

            Assert.True((expected.Text != null && actual.Text != null) || (expected.Text == null && actual.Text == null));

            if (expected.Text != null && actual.Text != null)
                AssertEqual((CategoryText)expected.Text, (CategoryText)actual.Text);

            AssertEqual<Category>(expected, actual);
        }

        public static void AssertEqual(CategoryText expected, CategoryText actual)
        {
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.MetaTitle, actual.MetaTitle);
            Assert.Equal(expected.MetaKeyword, actual.MetaKeyword);
            Assert.Equal(expected.MetaDescription, actual.MetaDescription);
            Assert.Equal(expected.LongDescription, actual.LongDescription);
        }

        public static void AssertEqual(Currency expected, Currency actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Symbol, actual.Symbol);

            AssertEqual<Currency>(expected, actual);
        }

        public static void AssertEqual(Grid expected, Grid actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);

            AssertEqual<Grid>(expected, actual);
        }

        public static void AssertEqual(HorizontalVariation expected, HorizontalVariation actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Interface, actual.Interface);
            Assert.Equal(expected.Color, actual.Color);
            Assert.Equal(expected.Order, actual.Order);
            Assert.Equal(expected.GridId, actual.GridId);

            AssertEqual<HorizontalVariation>(expected, actual);
        }

        public static void AssertEqual(PaymentForm expected, PaymentForm actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Active, actual.Active);
            Assert.Equal(expected.ProviderId, actual.ProviderId);

            AssertEqual<PaymentForm>(expected, actual);
        }

        public static void AssertEqual(PriceList expected, PriceList actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.InitDate, actual.InitDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.AdjustmentType, actual.AdjustmentType);
            Assert.Equal(expected.OperationType, actual.OperationType);
            Assert.Equal(expected.SaleMaxAmount, actual.SaleMaxAmount);
            Assert.Equal(expected.ParentId, actual.ParentId);

            AssertEqual<PriceList>(expected, actual);
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

        public static void AssertEqual(ProductAttribute expected, ProductAttribute actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.MetadataId, actual.MetadataId);
            Assert.Equal(expected.ProductId, actual.ProductId);

            AssertEqual<ProductAttribute>(expected, actual);
        }

        public static void AssertEqual(Provider expected, Provider actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.Contact, actual.Contact);
            Assert.Equal(expected.Site, actual.Site);
            Assert.Equal(expected.FiscalDocument, actual.FiscalDocument);
            Assert.Equal(expected.Document, actual.Document);
            Assert.Equal(expected.Active, actual.Active);
            Assert.Equal(expected.PersonType, actual.PersonType);
            Assert.Equal(expected.AddressId, actual.AddressId);

            Assert.True((expected.Address != null && actual.Address != null) || (expected.Address == null && actual.Address == null));

            if (expected.Address != null && actual.Address != null)
                AssertEqual((Address)expected.Address, (Address)actual.Address);

            AssertEqual<Provider>(expected, actual);
        }

        public static void AssertEqual(Section expected, Section actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);

            AssertEqual<Section>(expected, actual);
        }

        public static void AssertEqual(Sku expected, Sku actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.SkuCode, actual.SkuCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Reference, actual.Reference);
            Assert.Equal(expected.Bar, actual.Bar);
            Assert.Equal(expected.Height, actual.Height);
            Assert.Equal(expected.Width, actual.Width);
            Assert.Equal(expected.Depth, actual.Depth);
            Assert.Equal(expected.Weight, actual.Weight);
            Assert.Equal(expected.Cost, actual.Cost);
            Assert.Equal(expected.ProductId, actual.ProductId);

            AssertEqual<Sku>(expected, actual);
        }

        public static void AssertEqual(SkuPrice expected, SkuPrice actual)
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
            Assert.Equal(expected.SkuId, actual.SkuId);

            AssertEqual<SkuPrice>(expected, actual);
        }

        public static void AssertEqual(StockUnit expected, StockUnit actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Complement, actual.Complement);
            Assert.Equal(expected.AddressId, actual.AddressId);
            Assert.Equal(expected.CompanyId, actual.CompanyId);

            AssertEqual<StockUnit>(expected, actual);
        }

        public static void AssertEqual(User expected, User actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Login, actual.Login);
            Assert.Equal(expected.Password, actual.Password);
            Assert.Equal(expected.Email, actual.Email);
        }

        public static void AssertEqual(VerticalVariation expected, VerticalVariation actual)
        {
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.ExternalCode, actual.ExternalCode);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Interface, actual.Interface);
            Assert.Equal(expected.Order, actual.Order);
            Assert.Equal(expected.GridId, actual.GridId);

            AssertEqual<VerticalVariation>(expected, actual);
        }
    }
}
