using FluentValidation;
using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class AdvertisementValidation : AbstractValidator<IAdvertisement>
    {
        public AdvertisementValidation()
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

            RuleFor(x => x.IntegrationCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Código Integração é obrigatório")
                .Length(1, 50).WithMessage("O Código Integração deve ter até 50 caracteres");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(1, 300).WithMessage("O Nome deve ter até 300 caracteres");

            RuleFor(x => x.AdvertisementType)
                .NotEmpty().WithMessage("O Tipo Anúncio é obrigatório");

            When(x => !String.IsNullOrEmpty(x.AdvertisementType), () =>
            {
                RuleFor(x => x.AdvertisementType!.GetEnumValueFromDescription<TipoAnuncio>())
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(x => $"O Tipo Anúncio {x.AdvertisementType} está inválido")
                    .IsInEnum().WithMessage(x => $"O Tipo Anúncio {x.AdvertisementType} está inválido");
            });

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("O Tipo Status Anúncio é obrigatório");

            When(x => !String.IsNullOrEmpty(x.Status), () =>
            {
                RuleFor(x => x.Status!.GetEnumValueFromDescription<TipoStatusAnuncio>())
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(x => $"O Tipo Status Anúncio {x.Status} está inválido1")
                    .IsInEnum().WithMessage(x => $"O Tipo Status Anúncio {x.Status} está inválido2");
            });
        }
    }
}
