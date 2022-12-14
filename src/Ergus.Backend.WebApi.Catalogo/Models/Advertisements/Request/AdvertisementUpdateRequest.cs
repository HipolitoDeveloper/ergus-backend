using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Advertisements.Request
{
    public class AdvertisementUpdateRequest : BaseModel, IAdvertisement
    {
        public int Id                       { get; set; }
        public string? Code                 { get; set; } = string.Empty;
        public string? ExternalCode         { get; set; } = string.Empty;
        public string? SkuCode              { get; set; } = string.Empty;
        public string? IntegrationCode      { get; set; } = string.Empty;
        public string? Name                 { get; set; } = string.Empty;
        public string? AdvertisementType    { get; set; } = string.Empty;
        public string? Status               { get; set; } = string.Empty;
        public int? IntegrationId           { get; set; }
        public int? ProductId               { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdvertisementUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdvertisementUpdateRequestValidation : AbstractValidator<AdvertisementUpdateRequest>
    {
        public AdvertisementUpdateRequestValidation()
        {
            Include(new AdvertisementValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
