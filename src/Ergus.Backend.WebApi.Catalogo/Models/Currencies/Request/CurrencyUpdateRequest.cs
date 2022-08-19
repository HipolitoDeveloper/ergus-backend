using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Currencies.Request
{
    public class CurrencyUpdateRequest : BaseModel, ICurrency
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Symbol       { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new CurrencyUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CurrencyUpdateRequestValidation : AbstractValidator<CurrencyUpdateRequest>
    {
        public CurrencyUpdateRequestValidation()
        {
            Include(new CurrencyValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
