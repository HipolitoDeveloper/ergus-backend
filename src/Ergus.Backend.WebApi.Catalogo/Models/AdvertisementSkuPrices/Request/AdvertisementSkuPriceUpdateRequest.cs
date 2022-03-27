using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.AdvertisementSkuPrices.Request
{
    public class AdvertisementSkuPriceUpdateRequest : BaseModel, IAdvertisementSkuPrice
    {
        public int Id                   { get; set; }
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
            ValidationResult = new AdvertisementSkuPriceUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdvertisementSkuPriceUpdateRequestValidation : AbstractValidator<AdvertisementSkuPriceUpdateRequest>
    {
        public AdvertisementSkuPriceUpdateRequestValidation()
        {
            Include(new AdvertisementSkuPriceValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
