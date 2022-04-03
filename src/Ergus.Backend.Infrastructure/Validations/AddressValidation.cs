using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class AddressValidation : AbstractValidator<IAddress>
    {
        public AddressValidation(string prefixo = "")
        {
            RuleFor(x => x.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O Código é obrigatório")
                .Length(1, 50).WithMessage($"{prefixo}O Código deve ter até 50 caracteres");

            RuleFor(x => x.ExternalCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O Código Externo é obrigatório")
                .Length(1, 50).WithMessage($"{prefixo}O Código Externo deve ter até 50 caracteres");

            RuleFor(x => x.CityCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O Código da Cidade é obrigatório")
                .Length(1, 50).WithMessage($"{prefixo}O Código da Cidade deve ter até 50 caracteres");

            RuleFor(x => x.District)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O Bairro é obrigatório")
                .Length(1, 60).WithMessage($"{prefixo}O Bairro deve ter até 60 caracteres");

            RuleFor(x => x.Complement)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O Complemento é obrigatório")
                .Length(1, 300).WithMessage($"{prefixo}O Complemento deve ter até 300 caracteres");

            RuleFor(x => x.Number)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O Número é obrigatório")
                .Length(1, 100).WithMessage($"{prefixo}O v deve ter até 100 caracteres");

            RuleFor(x => x.Reference)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}A Reference é obrigatória")
                .Length(1, 100).WithMessage($"{prefixo}A Reference deve ter até 100 caracteres");

            RuleFor(x => x.ZipCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O CEP é obrigatório")
                .IsValidCEP().WithMessage($"{prefixo}O CEP está inválido")
                .Length(1, 9).WithMessage($"{prefixo}O CEP deve ter até 300 caracteres");

            RuleFor(x => x.AddressValue)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{prefixo}O Logradouro é obrigatório")
                .Length(1, 100).WithMessage($"{prefixo}O Logradouro deve ter até 100 caracteres");
        }
    }
}
