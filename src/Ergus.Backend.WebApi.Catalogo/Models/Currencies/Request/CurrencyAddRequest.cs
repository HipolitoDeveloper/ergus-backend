using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Ergus.Backend.WebApi.Catalogo.Models.Currencies.Request
{
    public class CurrencyAddRequest : BaseModel, ICurrency
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Symbol       { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new CurrencyAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CurrencyAddRequestValidation : AbstractValidator<CurrencyAddRequest>
    {
        public CurrencyAddRequestValidation()
        {
            Include(new CurrencyValidation());
        }
    }
}
