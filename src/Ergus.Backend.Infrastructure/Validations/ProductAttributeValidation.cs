using FluentValidation;
using Ergus.Backend.Infrastructure.Models.Interfaces;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class ProductAttributeValidation : AbstractValidator<IProductAttribute>
    {
        public ProductAttributeValidation()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código é obrigatório")
                .Length(1, 50).WithMessage("O Código deve ter até 50 caracteres");

            RuleFor(x => x.ExternalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Externo é obrigatório")
                .Length(1, 50).WithMessage("O Código Externo deve ter até 50 caracteres");
        }
    }
}
