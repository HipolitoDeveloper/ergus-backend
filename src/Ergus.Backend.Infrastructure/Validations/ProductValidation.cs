using FluentValidation;
using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;

namespace Ergus.Backend.Infrastructure.Validations
{
    public class ProductValidation : AbstractValidator<IProduct>
    {
        public ProductValidation()
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

            RuleFor(x => x.NCM)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O NCM é obrigatório")
                .Length(1, 10).WithMessage("O NCM deve ter até 10 caracteres");

            RuleFor(x => x.AdvertisementType)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Tipo Anúncio é obrigatório");

            When(x => !String.IsNullOrEmpty(x.AdvertisementType), () =>
            {
                RuleFor(x => x.AdvertisementType!.GetEnumValueFromDescription<TipoAnuncio>())
                    .NotEmpty().WithMessage(x => $"O Tipo Anúncio {x.AdvertisementType} está inválido")
                    .IsInEnum().WithMessage(x => $"O Tipo Anúncio {x.AdvertisementType} está inválido");
            });
        }
    }
}
