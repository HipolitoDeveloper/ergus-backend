using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Ergus.Backend.WebApi.Catalogo.Models.PaymentForms.Request
{
    public class PaymentFormAddRequest : BaseModel, IPaymentForm
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public int? ProviderId      { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new PaymentFormAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PaymentFormAddRequestValidation : AbstractValidator<PaymentFormAddRequest>
    {
        public PaymentFormAddRequestValidation()
        {
            Include(new PaymentFormValidation());
        }
    }
}
