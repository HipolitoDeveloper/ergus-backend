using Ergus.Backend.Infrastructure.Models.Interfaces;
using FluentValidation;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class CurrencyValidation : AbstractValidator<ICurrency>
    {
        public CurrencyValidation()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código é obrigatório")
                .Length(1, 50).WithMessage("O Código deve ter até 50 caracteres");

            RuleFor(x => x.ExternalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Externo é obrigatório")
                .Length(1, 50).WithMessage("O Código Externo deve ter até 50 caracteres");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(1, 200).WithMessage("O Nome deve ter até 200 caracteres");

            RuleFor(x => x.Symbol)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("A Sigla é obrigatória")
                .Length(1, 10).WithMessage("A Sigla deve ter até 10 caracteres");
        }
    }
}
