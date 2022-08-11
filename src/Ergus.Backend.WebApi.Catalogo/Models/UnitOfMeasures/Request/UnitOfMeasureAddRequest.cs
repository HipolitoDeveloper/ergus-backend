using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Ergus.Backend.WebApi.Catalogo.Models.UnitOfMeasures.Request
{
    public class UnitOfMeasureAddRequest : BaseModel, IUnitOfMeasure
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Description  { get; set; } = string.Empty;
        public string? Acronym      { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new UnitOfMeasureAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UnitOfMeasureAddRequestValidation : AbstractValidator<UnitOfMeasureAddRequest>
    {
        public UnitOfMeasureAddRequestValidation()
        {
            Include(new UnitOfMeasureValidation());
        }
    }
}
