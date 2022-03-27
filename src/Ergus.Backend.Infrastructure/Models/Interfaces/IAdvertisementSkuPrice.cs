namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IAdvertisementSkuPrice
    {
        string? Code                    { get; }
        string? ExternalCode            { get; }
        decimal Value                   { get; }
        decimal FictionalValue          { get; }
        DateTime? PromotionStart        { get; }
        DateTime? PromotionEnd          { get; }
        int? PriceListId                { get; }
        int? AdvertisementSkuId         { get; }

        bool EhValido();
    }
}
