using FluentValidation;
using Ergus.Backend.Infrastructure.Models.Interfaces;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class SkuValidation : AbstractValidator<ISku>
    {
        public SkuValidation()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código é obrigatório")
                .Length(1, 50).WithMessage("O Código deve ter até 50 caracteres");

            RuleFor(x => x.ExternalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Externo é obrigatório")
                .Length(1, 50).WithMessage("O Código Externo deve ter até 50 caracteres");

            RuleFor(x => x.SkuCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Sku é obrigatório")
                .Length(1, 50).WithMessage("O Código Sku deve ter até 50 caracteres");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(1, 100).WithMessage("O Nome deve ter até 100 caracteres");

            RuleFor(x => x.Reference)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Referência é obrigatório")
                .Length(1, 50).WithMessage("O Código Referência deve ter até 50 caracteres");

            RuleFor(x => x.Bar)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código de Barras é obrigatório")
                .Length(1, 20).WithMessage("O Código de Barras deve ter até 20 caracteres");

            RuleFor(x => x.Height)
                .ScalePrecision(2, 18).WithMessage("A altura deve ter no máximo 18 dígitos sendo 2 decimais");

            RuleFor(x => x.Width)
                .ScalePrecision(2, 18).WithMessage("A largura deve ter no máximo 18 dígitos sendo 2 decimais");

            RuleFor(x => x.Depth)
                .ScalePrecision(2, 18).WithMessage("A profundidade deve ter no máximo 18 dígitos sendo 2 decimais");

            RuleFor(x => x.Weight)
                .ScalePrecision(4, 18).WithMessage("O peso deve ter no máximo 18 dígitos sendo 4 decimais");

            RuleFor(x => x.Cost)
                .ScalePrecision(4, 18).WithMessage("O custo deve ter no máximo 18 dígitos sendo 4 decimais");
        }
    }
}
