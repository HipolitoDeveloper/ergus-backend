using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.SkuPrices.Request
{
    public class SkuPriceAddRequest : BaseModel, ISkuPrice
    {
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public decimal Value            { get; set; }
        public decimal FictionalValue   { get; set; }
        public DateTime? PromotionStart { get; set; }
        public DateTime? PromotionEnd   { get; set; }
        public int? PriceListId         { get; set; }
        public int? SkuId               { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new SkuPriceAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SkuPriceAddRequestValidation : AbstractValidator<SkuPriceAddRequest>
    {
        public SkuPriceAddRequestValidation()
        {
            Include(new SkuPriceValidation());
        }
    }
}
