using Ergus.Backend.Infrastructure.Models.Interfaces;
using FluentValidation;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class SectionValidation : AbstractValidator<ISection>
    {
        public SectionValidation()
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
                .Length(1, 100).WithMessage("O Nome deve ter até 100 caracteres");
        }
    }
}
