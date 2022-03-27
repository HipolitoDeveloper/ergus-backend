using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class PriceListValidation : AbstractValidator<IPriceList>
    {
        public PriceListValidation()
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código é obrigatório")
                .Length(1, 50).WithMessage("O Código deve ter até 50 caracteres");

            RuleFor(x => x.ExternalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Externo é obrigatório")
                .Length(1, 50).WithMessage("O Código Externo deve ter até 50 caracteres");

            RuleFor(u => u.InitDate)
                .IsValidDateTime(true).WithMessage("A Data Inicial está inválida");

            RuleFor(u => u.EndDate)
                .IsValidDateTime(true).WithMessage("A Data Final está inválida");

            When(u => u.InitDate > DateTime.MinValue && u.EndDate > DateTime.MinValue,
                () =>
                {
                    RuleFor(u => u.EndDate)
                     .Must((model, dataFim) => dataFim > model.InitDate).WithMessage("A Data Final deve ser posterior a Data Inicial");
                });

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(1, 50).WithMessage("O Nome deve ter até 50 caracteres");

            RuleFor(x => x.Value)
                .ScalePrecision(2, 18).WithMessage("O valor deve ter no máximo 18 dígitos sendo 2 decimais");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("O Tipo é obrigatório");

            When(x => !String.IsNullOrEmpty(x.Type), () =>
            {
                RuleFor(x => x.Type!.GetEnumValueFromDescription<TipoListaPreco>())
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(x => $"O Tipo {x.Type} está inválido")
                    .IsInEnum().WithMessage(x => $"O Tipo {x.Type} está inválido");
            });

            RuleFor(x => x.AdjustmentType)
                .NotEmpty().WithMessage("O Tipo de Ajuste é obrigatório");

            When(x => !String.IsNullOrEmpty(x.AdjustmentType), () =>
            {
                RuleFor(x => x.AdjustmentType!.GetEnumValueFromDescription<TipoAjuste>())
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(x => $"O Tipo de Ajuste {x.AdjustmentType} está inválido")
                    .IsInEnum().WithMessage(x => $"O Tipo de Ajuste {x.AdjustmentType} está inválido");
            });

            RuleFor(x => x.OperationType)
                .NotEmpty().WithMessage("O Tipo de Operação é obrigatório");

            When(x => !String.IsNullOrEmpty(x.OperationType), () =>
            {
                RuleFor(x => x.OperationType!.GetEnumValueFromDescription<TipoOperacao>())
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(x => $"O Tipo de Operação {x.OperationType} está inválido")
                    .IsInEnum().WithMessage(x => $"O Tipo de Operação {x.OperationType} está inválido");
            });

            RuleFor(u => u.SaleMaxAmount)
                .LessThan(Int32.MaxValue).WithMessage($"O valor máximo de venda é {Int32.MaxValue}.");
        }
    }
}
