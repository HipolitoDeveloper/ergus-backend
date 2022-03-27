using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.AdvertisementSkuPrices.Request
{
    public class AdvertisementSkuPriceAddRequest : BaseModel, IAdvertisementSkuPrice
    {
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public decimal Value            { get; set; }
        public decimal FictionalValue   { get; set; }
        public DateTime? PromotionStart { get; set; }
        public DateTime? PromotionEnd   { get; set; }
        public int? PriceListId         { get; set; }
        public int? AdvertisementSkuId  { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdvertisementSkuPriceAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdvertisementSkuPriceAddRequestValidation : AbstractValidator<AdvertisementSkuPriceAddRequest>
    {
        public AdvertisementSkuPriceAddRequestValidation()
        {
            Include(new AdvertisementSkuPriceValidation());
        }
    }
}
