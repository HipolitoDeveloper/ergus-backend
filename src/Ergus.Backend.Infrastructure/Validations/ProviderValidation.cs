using FluentValidation;
using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class ProviderValidation : AbstractValidator<IProvider<IAddress>>
    {
        public ProviderValidation()
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

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O E-mail é obrigatório")
                .EmailAddress().WithMessage("O E-mail está inválido")
                .Length(1, 150).WithMessage("O E-mail deve ter até 150 caracteres");

            RuleFor(x => x.Contact)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Contato é obrigatório")
                .Length(1, 100).WithMessage("O Contato deve ter até 100 caracteres");

            RuleFor(x => x.Site)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Site é obrigatório")
                .Length(1, 100).WithMessage("O Site deve ter até 100 caracteres");

            RuleFor(x => x.FiscalDocument)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Documento Fiscal é obrigatório")
                .Length(1, 20).WithMessage("O Documento Fiscal deve ter até 20 caracteres");

            RuleFor(x => x.Document)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Documento é obrigatório")
                .Length(1, 15).WithMessage("O Documento deve ter até 15 caracteres");

            RuleFor(x => x.PersonType)
                .NotEmpty().WithMessage("O Tipo Pessoa é obrigatório");

            When(x => !String.IsNullOrEmpty(x.PersonType), () =>
            {
                RuleFor(x => x.PersonType!.GetEnumValueFromDescription<TipoPessoa>())
                    .NotEmpty().WithMessage(x => $"O Tipo Pessoa {x.PersonType} está inválido")
                    .IsInEnum().WithMessage(x => $"O Tipo Pessoa {x.PersonType} está inválido");
            });

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address!).SetValidator(new AddressValidation("Address: "));
            });
        }
    }
}
