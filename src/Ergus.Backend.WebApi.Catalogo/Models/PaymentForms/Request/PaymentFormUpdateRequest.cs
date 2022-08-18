using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.PaymentForms.Request
{
    public class PaymentFormUpdateRequest : BaseModel, IPaymentForm
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public bool Active          { get; set; }
        public int? ProviderId      { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new PaymentFormUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PaymentFormUpdateRequestValidation : AbstractValidator<PaymentFormUpdateRequest>
    {
        public PaymentFormUpdateRequestValidation()
        {
            Include(new PaymentFormValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
