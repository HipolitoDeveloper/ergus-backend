using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.HorizontalVariations.Request
{
    public class HorizontalVariationUpdateRequest : BaseModel, IHorizontalVariation
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Interface    { get; set; } = string.Empty;
        public string? Color        { get; set; } = string.Empty;
        public int? Order           { get; set; }
        public int? GridId          { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new HorizontalVariationUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class HorizontalVariationUpdateRequestValidation : AbstractValidator<HorizontalVariationUpdateRequest>
    {
        public HorizontalVariationUpdateRequestValidation()
        {
            Include(new HorizontalVariationValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
