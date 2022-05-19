using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class SkuPriceResponse
    {
        public SkuPriceResponse(SkuPrice skuPrice)
        {
            if (skuPrice == null)
                return;

            this.Id = skuPrice.Id;
            this.Code = skuPrice.Code;
            this.ExternalCode = skuPrice.ExternalCode;
            this.Value = skuPrice.Value;
            this.FictionalValue = skuPrice.FictionalValue;
            this.PromotionStart = skuPrice.PromotionStart;
            this.PromotionEnd = skuPrice.PromotionEnd;
            this.PriceListId = skuPrice.PriceListId;
            this.SkuId = skuPrice.SkuId;

            this.Active = !skuPrice.WasRemoved;

            this.PriceList = skuPrice.PriceList == null ? null : new PriceListResponse(skuPrice.PriceList!);
            this.Sku = skuPrice.Sku == null ? null : new SkuResponse(skuPrice.Sku!);
        }

        public int Id                   { get; set; }
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public decimal Value            { get; set; }
        public decimal FictionalValue   { get; set; }
        public DateTime? PromotionStart { get; set; }
        public DateTime? PromotionEnd   { get; set; }
        public int? PriceListId         { get; set; }
        public int? SkuId               { get; set; }

        public bool Active              { get; set; }

        public virtual PriceListResponse? PriceList     { get; set; }
        public virtual SkuResponse? Sku                 { get; set; }
    }
}
