using Ergus.Backend.Infrastructure.Models.Interfaces;
using FluentValidation;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class UnitOfMeasureValidation : AbstractValidator<IUnitOfMeasure>
    {
        public UnitOfMeasureValidation()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código é obrigatório")
                .Length(1, 50).WithMessage("O Código deve ter até 50 caracteres");

            RuleFor(x => x.ExternalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Externo é obrigatório")
                .Length(1, 50).WithMessage("O Código Externo deve ter até 50 caracteres");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("A Descrição é obrigatória")
                .Length(1, 100).WithMessage("A Descrição deve ter até 200 caracteres");

            RuleFor(x => x.Acronym)
                .Length(0, 10).WithMessage("A Sigla deve ter até 10 caracteres");
        }
    }
}
