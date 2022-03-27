using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.SkuPrices.Request
{
    public class SkuPriceUpdateRequest : BaseModel, ISkuPrice
    {
        public int Id                   { get; set; }
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
            ValidationResult = new SkuPriceUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SkuPriceUpdateRequestValidation : AbstractValidator<SkuPriceUpdateRequest>
    {
        public SkuPriceUpdateRequestValidation()
        {
            Include(new SkuPriceValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
