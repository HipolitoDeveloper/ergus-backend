namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ISkuPrice
    {
        string? Code                { get; }
        string? ExternalCode        { get; }
        decimal Value               { get; }
        decimal FictionalValue      { get; }
        DateTime? PromotionStart    { get; }
        DateTime? PromotionEnd      { get; }
        int? PriceListId            { get; }
        int? SkuId                  { get; }

        bool EhValido();
    }
}
