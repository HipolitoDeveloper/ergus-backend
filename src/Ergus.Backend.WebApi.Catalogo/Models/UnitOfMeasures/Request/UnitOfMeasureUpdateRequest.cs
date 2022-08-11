using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.UnitOfMeasures.Request
{
    public class UnitOfMeasureUpdateRequest : BaseModel, IUnitOfMeasure
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Description  { get; set; } = string.Empty;
        public string? Acronym      { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new UnitOfMeasureUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UnitOfMeasureUpdateRequestValidation : AbstractValidator<UnitOfMeasureUpdateRequest>
    {
        public UnitOfMeasureUpdateRequestValidation()
        {
            Include(new UnitOfMeasureValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
