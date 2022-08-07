using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.VerticalVariations.Request
{
    public class VerticalVariationUpdateRequest : BaseModel, IVerticalVariation
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Interface    { get; set; } = string.Empty;
        public int? Order           { get; set; }
        public int? GridId          { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new VerticalVariationUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class VerticalVariationUpdateRequestValidation : AbstractValidator<VerticalVariationUpdateRequest>
    {
        public VerticalVariationUpdateRequestValidation()
        {
            Include(new VerticalVariationValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
