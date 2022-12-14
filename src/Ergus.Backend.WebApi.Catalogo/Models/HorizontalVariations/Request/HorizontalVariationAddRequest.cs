using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Ergus.Backend.WebApi.Catalogo.Models.HorizontalVariations.Request
{
    public class HorizontalVariationAddRequest : BaseModel, IHorizontalVariation
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Interface    { get; set; } = string.Empty;
        public string? Color        { get; set; } = string.Empty;
        public int? Order           { get; set; }
        public int? GridId          { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new HorizontalVariationAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class HorizontalVariationAddRequestValidation : AbstractValidator<HorizontalVariationAddRequest>
    {
        public HorizontalVariationAddRequestValidation()
        {
            Include(new HorizontalVariationValidation());
        }
    }
}
