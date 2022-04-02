using FluentValidation;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class CategoryValidation : AbstractValidator<ICategory<ICategoryText>>
    {
        public CategoryValidation()
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

            When(x => x.Text != null, () =>
            {
                RuleFor(x => x.Text)
                    .Must(x => x!.EhValido());
            });
        }
    }
}
