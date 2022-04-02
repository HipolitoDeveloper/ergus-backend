using FluentValidation;
using Ergus.Backend.Infrastructure.Models.Interfaces;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class CategoryTextValidation : AbstractValidator<ICategoryText>
    {
        public CategoryTextValidation()
        {
            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("A Descrição é obrigatória")
                .Length(1, 500).WithMessage("O Código deve ter até 500 caracteres");

            RuleFor(x => x.MetaTitle)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Meta Title é obrigatório")
                .Length(1, 300).WithMessage("O Meta Title deve ter até 300 caracteres");

            RuleFor(x => x.MetaKeyword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Meta Keyword é obrigatório")
                .Length(1, 3000).WithMessage("O Meta Keyword deve ter até 3000 caracteres");

            RuleFor(x => x.MetaDescription)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Meta Description é obrigatório")
                .Length(1, 3000).WithMessage("O Meta Description deve ter até 3000 caracteres");
        }
    }
}
