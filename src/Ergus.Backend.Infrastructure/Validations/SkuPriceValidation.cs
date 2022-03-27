using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class SkuPriceValidation : AbstractValidator<ISkuPrice>
    {
        public SkuPriceValidation()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código é obrigatório")
                .Length(1, 50).WithMessage("O Código deve ter até 50 caracteres");

            RuleFor(x => x.ExternalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Externo é obrigatório")
                .Length(1, 50).WithMessage("O Código Externo deve ter até 50 caracteres");

            RuleFor(x => x.Value)
                .ScalePrecision(2, 18).WithMessage("O valor deve ter no máximo 18 dígitos sendo 2 decimais");

            RuleFor(x => x.FictionalValue)
                .ScalePrecision(2, 18).WithMessage("O valor fictício deve ter no máximo 18 dígitos sendo 2 decimais");

            RuleFor(u => u.PromotionStart)
                .IsValidDateTime(false).WithMessage("A Data de Promoção Inicial está inválida");

            RuleFor(u => u.PromotionEnd)
                .IsValidDateTime(false).WithMessage("A Data de Promoção Final está inválida");

            When(u => u.PromotionStart.HasValue && u.PromotionEnd.HasValue && u.PromotionStart > DateTime.MinValue && u.PromotionEnd > DateTime.MinValue,
                () =>
                {
                    RuleFor(u => u.PromotionEnd)
                     .Must((model, dataFim) => dataFim > model.PromotionStart).WithMessage("A Data de Promoção Final deve ser posterior a Data de Promoção Inicial");
                });
        }
    }
}
