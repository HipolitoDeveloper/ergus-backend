using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class AdvertisementSkuPriceResponse
    {
        public AdvertisementSkuPriceResponse(AdvertisementSkuPrice advertisementSkuPrice)
        {
            if (advertisementSkuPrice == null)
                return;

            this.Id = advertisementSkuPrice.Id;
            this.Code = advertisementSkuPrice.Code;
            this.ExternalCode = advertisementSkuPrice.ExternalCode;
            this.Value = advertisementSkuPrice.Value;
            this.FictionalValue = advertisementSkuPrice.FictionalValue;
            this.PromotionStart = advertisementSkuPrice.PromotionStart;
            this.PromotionEnd = advertisementSkuPrice.PromotionEnd;
            this.PriceListId = advertisementSkuPrice.PriceListId;
            this.AdvertisementSkuId = advertisementSkuPrice.AdvertisementSkuId;

            this.PriceList = advertisementSkuPrice.PriceList == null ? null : new PriceListResponse(advertisementSkuPrice.PriceList!);
            this.AdvertisementSku = advertisementSkuPrice.AdvertisementSku == null ? null : new AdvertisementSkuResponse(advertisementSkuPrice.AdvertisementSku!);
        }

        public int Id                   { get; set; }
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public decimal Value            { get; set; }
        public decimal FictionalValue   { get; set; }
        public DateTime? PromotionStart { get; set; }
        public DateTime? PromotionEnd   { get; set; }
        public int? PriceListId         { get; set; }
        public int? AdvertisementSkuId  { get; set; }

        public virtual PriceListResponse? PriceList                 { get; set; }
        public virtual AdvertisementSkuResponse? AdvertisementSku   { get; set; }
    }
}
